﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Spect.Net.SpectrumEmu.Abstraction.Configuration;
using Spect.Net.SpectrumEmu.Abstraction.Devices;
using Spect.Net.SpectrumEmu.Abstraction.Providers;
using Spect.Net.SpectrumEmu.Cpu;
using Spect.Net.SpectrumEmu.Devices.Beeper;
using Spect.Net.SpectrumEmu.Devices.Interrupt;
using Spect.Net.SpectrumEmu.Devices.Keyboard;
using Spect.Net.SpectrumEmu.Devices.Memory;
using Spect.Net.SpectrumEmu.Devices.Rom;
using Spect.Net.SpectrumEmu.Devices.Screen;
using Spect.Net.SpectrumEmu.Devices.Sound;
using Spect.Net.SpectrumEmu.Devices.Tape;

#pragma warning disable 67

// ReSharper disable ConvertToAutoProperty
// ReSharper disable VirtualMemberCallInConstructor

namespace Spect.Net.SpectrumEmu.Machine
{
    /// <summary>
    /// This class represents a ZX Spectrum 48 virtual machine
    /// </summary>
    public class Spectrum48: ISpectrumVm, 
        ISpectrumVmTestSupport,
        ISpectrumVmRunCodeSupport
    {
        private readonly int _frameTacts;
        private bool _frameCompleted;
        private readonly List<ISpectrumBoundDevice> _spectrumDevices = new List<ISpectrumBoundDevice>();
        private readonly List<IFrameBoundDevice> _frameBoundDevices;
        private readonly List<ICpuOperationBoundDevice> _cpuBoundDevices;
        private ushort? _lastBreakpoint;

        /// <summary>
        /// The CPU tick at which the last frame rendering started;
        /// </summary>
        public long LastFrameStartCpuTick;

        /// <summary>
        /// The last rendered ULA tact 
        /// </summary>
        public int LastRenderedUlaTact;

        /// <summary>
        /// The length of the physical frame in clock counts
        /// </summary>
        public double PhysicalFrameClockCount { get; }

        /// <summary>
        /// Collection of RSpectrum devices
        /// </summary>
        public DeviceInfoCollection DeviceData { get; }
        
        /// <summary>
        /// The Z80 CPU of the machine
        /// </summary>
        public IZ80Cpu Cpu { get; }

        /// <summary>
        /// The current execution cycle options
        /// </summary>
        public ExecuteCycleOptions ExecuteCycleOptions { get; private set; }

        /// <summary>
        /// The configuration of the ROM
        /// </summary>
        public IRomConfiguration RomConfiguration { get; }

        /// <summary>
        /// The ROM provider object
        /// </summary>
        public IRomProvider RomProvider { get; }

        /// <summary>
        /// The ROM device used by the virtual machine
        /// </summary>
        public IRomDevice RomDevice { get; }

        /// <summary>
        /// The configuration of the memory
        /// </summary>
        public IMemoryConfiguration MemoryConfiguration { get; }

        /// <summary>
        /// The memory device used by the virtual machine
        /// </summary>
        public IMemoryDevice MemoryDevice { get; }

        /// <summary>
        /// The port device used by the virtual machine
        /// </summary>
        public IPortDevice PortDevice { get; }

        /// <summary>
        /// The clock used within the VM
        /// </summary>
        public IClockProvider Clock { get; }

        /// <summary>
        /// The configuration of the screen
        /// </summary>
        public ScreenConfiguration ScreenConfiguration { get; }

        /// <summary>
        /// The ULA device that renders the VM screen
        /// </summary>
        public IScreenDevice ScreenDevice { get; }

        /// <summary>
        /// The ULA device that takes care of raising interrupts
        /// </summary>
        public IInterruptDevice InterruptDevice { get; }

        /// <summary>
        /// The device responsible for handling the keyboard
        /// </summary>
        public IKeyboardDevice KeyboardDevice { get; }

        /// <summary>
        /// The provider that handles the keyboard
        /// </summary>
        public IKeyboardProvider KeyboardProvider { get; }

        /// <summary>
        /// The beeper device attached to the VM
        /// </summary>
        public IBeeperDevice BeeperDevice { get; }

        /// <summary>
        /// The provider that handles the beeper
        /// </summary>
        public IBeeperProvider BeeperProvider { get; }

        /// <summary>
        /// Beeper configuration
        /// </summary>
        public IAudioConfiguration AudioConfiguration { get; }

        /// <summary>
        /// The sound device attached to the VM
        /// </summary>
        public ISoundDevice SoundDevice { get; }

        /// <summary>
        /// The provider that handles the sound
        /// </summary>
        public ISoundProvider SoundProvider { get; }

        /// <summary>
        /// Sound configuration
        /// </summary>
        public IAudioConfiguration SoundConfiguration { get; }

        /// <summary>
        /// The tape device attached to the VM
        /// </summary>
        public ITapeDevice TapeDevice { get; }

        /// <summary>
        /// Debug info provider object
        /// </summary>
        public ISpectrumDebugInfoProvider DebugInfoProvider { get; set; }

        /// <summary>
        /// Gets the optional link to the virtual machine controller
        /// </summary>
        public IVmControlLink VmControlLink { get; }

        /// <summary>
        /// #of frames rendered
        /// </summary>
        public int FrameCount { get; private set; }

        /// <summary>
        /// #of tacts within the frame
        /// </summary>
        public int FrameTacts => _frameTacts;

        /// <summary>
        /// Gets the current frame tact according to the CPU tick count
        /// </summary>
        public virtual int CurrentFrameTact => (int)(Cpu.Tacts - LastFrameStartCpuTick)/ClockMultiplier;

        /// <summary>
        /// Overflow from the previous frame, given in #of tacts 
        /// </summary>
        public int Overflow { get; set; }

        /// <summary>
        /// The number of frame tact at which the interrupt signal is generated
        /// </summary>
        public int InterruptTact => ScreenConfiguration.InterruptTact;

        /// <summary>
        /// This property indicates if the machine currently runs the
        /// maskable interrupt method.
        /// </summary>
        public bool RunsInMaskableInterrupt { get; private set; }

        /// <summary>
        /// Allows to set a clock frequency multiplier value (1, 2, 4, or 8).
        /// </summary>
        public int ClockMultiplier { get; }

        /// <summary>
        /// Initializes a class instance using a collection of devices
        /// </summary>
        public Spectrum48(DeviceInfoCollection deviceData, IVmControlLink controlLink = null)
        {
            DeviceData = deviceData ?? throw new ArgumentNullException(nameof(deviceData));

            // --- Prepare the memory device
            var memoryInfo = GetDeviceInfo<IMemoryDevice>();
            MemoryDevice = memoryInfo?.Device ?? new Spectrum48MemoryDevice();
            MemoryConfiguration = (IMemoryConfiguration) memoryInfo?.ConfigurationData;

            // --- Prepare the port device
            var portInfo = GetDeviceInfo<IPortDevice>();
            PortDevice = portInfo?.Device ?? new Spectrum48PortDevice();

            // --- Init the CPU 
            var cpuConfig = GetDeviceConfiguration<IZ80Cpu,ICpuConfiguration>();
            var mult = 1;
            if (cpuConfig != null)
            {
                BaseClockFrequency = cpuConfig.BaseClockFrequency;
                mult = cpuConfig.ClockMultiplier;
                if (mult < 1) mult = 1;
                else if (mult >= 2 && mult <= 3) mult = 2;
                else if (mult >= 4 && mult <= 7) mult = 4;
                else if (mult > 8) mult = 8;
            }
            ClockMultiplier = mult;
            Cpu = new Z80Cpu(MemoryDevice, PortDevice, cpuConfig?.SupportsNextOperations ?? false)
            {
                UseGateArrayContention = MemoryConfiguration.ContentionType == MemoryContentionType.GateArray
            };

            // --- Init the ROM
            var romInfo = GetDeviceInfo<IRomDevice>();
            RomProvider = (IRomProvider)romInfo.Provider;
            RomDevice = romInfo.Device ?? new SpectrumRomDevice();
            RomConfiguration = (IRomConfiguration)romInfo.ConfigurationData;

            // --- Init the clock
            var clockInfo = GetDeviceInfo<IClockDevice>();
            Clock = (IClockProvider) clockInfo.Provider 
                ?? throw new InvalidOperationException("The virtual machine needs a clock provider!");

            // --- Init the screen device
            var screenInfo = GetDeviceInfo<IScreenDevice>();
            var pixelRenderer = (IScreenFrameProvider) screenInfo.Provider;
            ScreenConfiguration = new ScreenConfiguration((IScreenConfiguration)screenInfo.ConfigurationData);
            ScreenDevice = screenInfo.Device ?? new Spectrum48ScreenDevice();

            // --- Init the beeper device
            var beeperInfo = GetDeviceInfo<IBeeperDevice>();
            AudioConfiguration = (IAudioConfiguration)beeperInfo?.ConfigurationData;
            BeeperProvider = (IBeeperProvider) beeperInfo?.Provider;
            BeeperDevice = beeperInfo?.Device ?? new BeeperDevice();

            // --- Init the keyboard device
            var keyboardInfo = GetDeviceInfo<IKeyboardDevice>();
            KeyboardProvider = (IKeyboardProvider) keyboardInfo?.Provider;
            KeyboardDevice = keyboardInfo?.Device ?? new KeyboardDevice();

            // --- Init the interrupt device
            InterruptDevice = new InterruptDevice(InterruptTact);

            // --- Init the tape device
            var tapeInfo = GetDeviceInfo<ITapeDevice>();
            var tapeProvider = (ITapeProvider) tapeInfo?.Provider;
            TapeDevice = tapeInfo?.Device 
                ?? new TapeDevice(tapeProvider);

            // === Init optional devices
            // --- Init the sound device
            var soundInfo = GetDeviceInfo<ISoundDevice>();
            SoundConfiguration = (IAudioConfiguration)soundInfo?.ConfigurationData;
            SoundProvider = (ISoundProvider)soundInfo?.Provider;
            SoundDevice = soundInfo == null
                ? null
                : soundInfo.Device ?? new SoundDevice();


            // --- Set up Spectrum devices
            VmControlLink = controlLink;

            // --- Carry out frame calculations
            ResetUlaTact();
            _frameTacts = ScreenConfiguration.ScreenRenderingFrameTactCount;
            PhysicalFrameClockCount = Clock.GetFrequency() / (double)BaseClockFrequency * _frameTacts;
            FrameCount = 0;
            Overflow = 0;
            _frameCompleted = true;
            _lastBreakpoint = null;
            RunsInMaskableInterrupt = false;

            // --- Attach providers
            AttachProvider(RomProvider);
            AttachProvider(Clock);
            AttachProvider(pixelRenderer);
            AttachProvider(BeeperProvider);
            AttachProvider(KeyboardProvider);
            AttachProvider(tapeProvider);
            AttachProvider(DebugInfoProvider);
            
            // --- Attach optional providers
            if (SoundProvider != null)
            {
                AttachProvider(SoundProvider);
            }

            // --- Collect Spectrum devices
            _spectrumDevices.Add(RomDevice);
            _spectrumDevices.Add(MemoryDevice);
            _spectrumDevices.Add(PortDevice);
            _spectrumDevices.Add(ScreenDevice);
            _spectrumDevices.Add(BeeperDevice);
            _spectrumDevices.Add(KeyboardDevice);
            _spectrumDevices.Add(InterruptDevice);
            _spectrumDevices.Add(TapeDevice);

            // --- Collect optional devices
            if (SoundDevice != null)
            {
                _spectrumDevices.Add(SoundDevice);
            }

            // --- Now, prepare devices to find each other
            foreach (var device in _spectrumDevices)
            {
                device.OnAttachedToVm(this);
            }

            // --- Prepare bound devices
            _frameBoundDevices = _spectrumDevices
                .OfType<IFrameBoundDevice>()
                .ToList();
            _cpuBoundDevices = _spectrumDevices
                .OfType<ICpuOperationBoundDevice>()
                .ToList();

            DebugInfoProvider = new SpectrumDebugInfoProvider();

            // --- Init the ROM
            InitRom(RomDevice, RomConfiguration);
        }

        /// <summary>
        /// Attache the specified provider to this VM
        /// </summary>
        private void AttachProvider(IVmComponentProvider provider)
        {
            provider?.OnAttachedToVm(this);
        }

        /// <summary>
        /// Gets the device with the provided type
        /// </summary>
        /// <typeparam name="TDevice"></typeparam>
        /// <returns></returns>
        public IDeviceInfo<TDevice, IDeviceConfiguration, IVmComponentProvider> GetDeviceInfo<TDevice>()
            where TDevice : class, IDevice
        {
            return DeviceData.TryGetValue(typeof(TDevice), out var deviceInfo)
                ? (IDeviceInfo<TDevice, IDeviceConfiguration, IVmComponentProvider>)deviceInfo
                : null;
        }

        /// <summary>
        /// Gets the device with the provided type
        /// </summary>
        /// <typeparam name="TDevice"></typeparam>
        /// <returns></returns>
        public TDevice GetDevice<TDevice>()
            where TDevice: class, IDevice
        {
            return DeviceData.TryGetValue(typeof(TDevice), out var deviceInfo)
                ? (TDevice) deviceInfo.Device
                : null;
        }

        /// <summary>
        /// Gets the device with the provided type
        /// </summary>
        /// <typeparam name="TDevice">Device type</typeparam>
        /// <typeparam name="TConfig">Configuration type</typeparam>
        /// <returns></returns>
        public TConfig GetDeviceConfiguration<TDevice, TConfig>()
            where TConfig : class, IDeviceConfiguration
        {
            return DeviceData.TryGetValue(typeof(TDevice), out var deviceInfo)
                ? (TConfig)deviceInfo.ConfigurationData
                : null;
        }

        /// <summary>
        /// Resets the CPU and the ULA chip
        /// </summary>
        public void Reset()
        {
            Cpu.SetResetSignal();
            ResetUlaTact();
            FrameCount = 0;
            Overflow = 0;
            _frameCompleted = true;
            Cpu.Reset();
            Cpu.ReleaseResetSignal();
            RunsInMaskableInterrupt = false;
            foreach (var device in _spectrumDevices)
            {
                device.Reset();
            }
            if (DebugInfoProvider != null)
            {
                DebugInfoProvider.ImminentBreakpoint = null;
            }
        }

        /// <summary>
        /// Allow the device to react to the start of a new frame
        /// </summary>
        public void OnNewFrame()
        {
            foreach (var device in _frameBoundDevices)
            {
                device.OnNewFrame();
            }
        }

        /// <summary>
        /// Allow the device to react to the completion of a frame
        /// </summary>
        public void OnFrameCompleted()
        {
            foreach (var device in _frameBoundDevices)
            {
                device.Overflow = Overflow;
                device.OnFrameCompleted();
            }
        }

        public event EventHandler FrameCompleted;

        /// <summary>
        /// Resets the ULA tact to start screen rendering from the beginning
        /// </summary>
        public void ResetUlaTact()
        {
            LastRenderedUlaTact = -1;
        }

        /// <summary>
        /// Sets the debug info provider to the specified object
        /// </summary>
        /// <param name="provider">Provider object</param>
        public void SetDebugInfoProvider(ISpectrumDebugInfoProvider provider)
        {
            DebugInfoProvider = provider;
        }

        /// <summary>
        /// The main execution cycle of the Spectrum VM
        /// </summary>
        /// <param name="token">Cancellation token</param>
        /// <param name="options">Execution options</param>
        /// <return>True, if the cycle completed; false, if it has been cancelled</return>
        public bool ExecuteCycle(CancellationToken token, ExecuteCycleOptions options)
        {
            ExecuteCycleOptions = options;

            // --- We use these variables to calculate wait time at the end of the frame
            var cycleStartTime = Clock.GetCounter(); 
            var cycleFrameCount = 0;

            // --- We use this variable to check whether to stop in Debug mode
            var executedInstructionCount = -1;

            // --- Notify the controller that the vm successfully started
            VmControlLink?.ExecutionCycleStarted();

            // --- Loop #1: The main cycle that goes on until cancelled
            while (!token.IsCancellationRequested)
            {
                if (_frameCompleted)
                {
                    // --- This counter helps us to calculate where we are in the frame after
                    // --- each CPU operation cycle
                    LastFrameStartCpuTick = Cpu.Tacts - Overflow;

                    // --- Notify devices to start a new frame
                    OnNewFrame();
                    LastRenderedUlaTact = Overflow;
                    _frameCompleted = false;
                }

                // --- Loop #2: The physical frame cycle that goes on while CPU and ULA 
                // --- processes everything whithin a physical frame (0.019968 second)
                while (!_frameCompleted)
                {
                    // --- Check for leaving maskable interrupt mode
                    if (RunsInMaskableInterrupt)
                    {
                        if (Cpu.Registers.PC == 0x0052)
                        {
                            // --- We leave the maskable interrupt mode when the
                            // --- current instruction completes
                            RunsInMaskableInterrupt = false;
                        }
                    }

                    // --- Check debug mode when a CPU instruction has been entirelly executed
                    if (!Cpu.IsInOpExecution)
                    {
                        // --- Check for cancellation
                        if (token.IsCancellationRequested)
                        {
                            return false;
                        }

                        // --- The next instruction is about to be executed
                        executedInstructionCount++;

                        // --- Check for reaching the termination point
                        if (options.EmulationMode == EmulationMode.UntilExecutionPoint
                            && options.TerminationRom == MemoryDevice.GetSelectedRomIndex()
                            && options.TerminationPoint == Cpu.Registers.PC)
                        {
                            // --- We reached the termination point
                            return true;
                        }

                        // --- Check for entering maskable interrupt mode
                        if (Cpu.MaskableInterruptModeEntered)
                        {
                            RunsInMaskableInterrupt = true;
                        }

                        // --- Check for a debugging stop point
                        if (options.EmulationMode == EmulationMode.Debugger)
                        {
                            if (IsDebugStop(options, executedInstructionCount))
                            {
                                // --- At this point, the cycle should be stopped because of debugging reasons
                                // --- The screen should be refreshed
                                ScreenDevice.OnFrameCompleted();
                                return true;
                            }
                        }
                    }

                    // --- Check for interrupt signal generation
                    InterruptDevice.CheckForInterrupt(CurrentFrameTact);

                    // --- Run a single Z80 instruction
                    Cpu.ExecuteCpuCycle();
                    _lastBreakpoint = null;

                    // --- Run a rendering cycle according to the current CPU tact count
                    var lastTact = CurrentFrameTact;
                    ScreenDevice.RenderScreen(LastRenderedUlaTact + 1, lastTact);
                    LastRenderedUlaTact = lastTact;

                    // --- Exit if the emulation mode specifies so
                    if (options.EmulationMode == EmulationMode.UntilHalt 
                        && (Cpu.StateFlags & Z80StateFlags.Halted) != 0)
                    {
                        return true;
                    }

                    // --- Notify each CPU-bound device that the current operation has been completed
                    foreach (var device in _cpuBoundDevices)
                    {
                        device.OnCpuOperationCompleted();
                    }

                    // --- Decide whether this frame has been completed
                    _frameCompleted = !Cpu.IsInOpExecution && CurrentFrameTact >= _frameTacts;

                } // -- End Loop #2

                // --- A physical frame has just been completed. Take care about screen refresh
                cycleFrameCount++;
                FrameCount++;

                // --- Notify devices that the current frame completed
                OnFrameCompleted();

                // --- Exit if the emulation mode specifies so
                if (options.EmulationMode == EmulationMode.UntilFrameEnds)
                {
                    return true;
                }

                // --- Wait while the frame time ellapses
                var nextFrameCounter = cycleStartTime + cycleFrameCount * PhysicalFrameClockCount;
                Clock.WaitUntil((long) nextFrameCounter, token);

                // --- Start a new frame and carry on
                Overflow = CurrentFrameTact % _frameTacts;

            } // --- End Loop #1

            // --- The cycle has been interrupted by cancellation
            return false;
        }

        /// <summary>
        /// Checks whether the execution cycle should be stopped for debugging
        /// </summary>
        /// <param name="options">Execution options</param>
        /// <param name="executedInstructionCount">
        /// The count of instructions already executed in this cycle
        /// </param>
        /// <returns>True, if the execution should be stopped</returns>
        private bool IsDebugStop(ExecuteCycleOptions options, int executedInstructionCount)
        {
            // --- No debug provider, no stop
            if (DebugInfoProvider == null)
            {
                return false;
            }

            // Check if the maskable interrupt routine breakpoints should be skipped
            if (RunsInMaskableInterrupt)
            {
                if (options.SkipInterruptRoutine) return false;
            }

            // --- In Step-Into mode we always stop when we're about to
            // --- execute the next instruction
            if (options.DebugStepMode == DebugStepMode.StepInto)
            {
                return executedInstructionCount > 0;
            }

            // --- In Stop-At-Breakpoint mode we stop only if a predefined
            // --- breakpoint is reached
            if (options.DebugStepMode == DebugStepMode.StopAtBreakpoint
                && DebugInfoProvider.ShouldBreakAtAddress(Cpu.Registers.PC))
            {
                if (executedInstructionCount > 0
                    || _lastBreakpoint == null
                    || _lastBreakpoint != Cpu.Registers.PC)
                {
                    // --- If we are paused at a breakpoint, we do not want
                    // --- to pause again and again, unless we step through
                    _lastBreakpoint = Cpu.Registers.PC;
                    return true;
                }
            }

            // --- We're in Step-Over mode
            if (options.DebugStepMode == DebugStepMode.StepOver)
            {
                if (DebugInfoProvider.ImminentBreakpoint != null)
                {
                    // --- We also stop, if an imminent breakpoint is reached, and also remove
                    // --- this breakpoint
                    if (DebugInfoProvider.ImminentBreakpoint == Cpu.Registers.PC)
                    {
                        DebugInfoProvider.ImminentBreakpoint = null;
                        return true;
                    }
                }
                else
                {
                    var imminentJustCreated = false;

                    // --- We check for a CALL-like instruction
                    var length = Cpu.GetCallInstructionLength();
                    if (length > 0)
                    {
                        // --- Its a CALL-like instraction, create an imminent breakpoint
                        DebugInfoProvider.ImminentBreakpoint = (ushort)(Cpu.Registers.PC + length);
                        imminentJustCreated = true;
                    }

                    // --- We stop, we executed at least one instruction and if there's no imminent 
                    // --- breakpoint or we've just created one
                    if (executedInstructionCount > 0
                        && (DebugInfoProvider.ImminentBreakpoint == null || imminentJustCreated))
                    {
                        return true;
                    }
                }
            }

            // --- In any other case, we carry on
            return false;
        }

        /// <summary>
        /// This flag tells if the frame has just been completed.
        /// </summary>
        public bool HasFrameCompleted => _frameCompleted;

        /// <summary>
        /// Writes a byte to the memory
        /// </summary>
        /// <param name="addr">Memory address</param>
        /// <param name="value">Data byte</param>
        public void WriteSpectrumMemory(ushort addr, byte value) =>
            MemoryDevice.Write(addr, value);

        /// <summary>
        /// Gets the frequency of the virtual machine's clock in Hz
        /// </summary>
        public int BaseClockFrequency { get; } = 3_500_000;

        #region ISpectrumVmRunCodeSupport

        /// <summary>
        /// Injects code into the memory
        /// </summary>
        /// <param name="addr">Start address</param>
        /// <param name="code">Code to inject</param>
        /// <remarks>The code leaves the ROM area untouched.</remarks>
        public void InjectCodeToMemory(ushort addr, IReadOnlyCollection<byte> code)
        {
            foreach (var codeByte in code)
            {
                MemoryDevice.Write(addr++, codeByte);
            }
        }

        /// <summary>
        /// Prepares the custom code for running, as if it were started
        /// with the RUN command
        /// </summary>
        public void PrepareRunMode()
        {
            // --- Set the keyboard in "L" mode
            var flags = MemoryDevice.Read(0x5C3B);
            flags |= 0x08;
            MemoryDevice.Write(0x5C3B, flags);

            // --- Allow interrupts
            RunsInMaskableInterrupt = false;
        }

        #endregion

        #region Helper functions

        /// <summary>
        /// Loads the content of the ROM through the specified provider
        /// </summary>
        /// <param name="romDevice">ROM device instance</param>
        /// <param name="romConfig">ROM configuration</param>
        /// <remarks>
        /// The content of the ROM is copied into the memory
        /// </remarks>
        public void InitRom(IRomDevice romDevice, IRomConfiguration romConfig)
        {
            for (var i = 0; i < romConfig.NumberOfRoms; i++)
            {
                MemoryDevice.SelectRom(i);
                MemoryDevice.CopyRom(romDevice.GetRomBytes(i));
            }
            MemoryDevice.SelectRom(0);
        }

        #endregion
    }

#pragma warning restore
}