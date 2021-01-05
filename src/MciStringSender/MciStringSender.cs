namespace Vurdalakov.MciStringSender
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;

    public class MciStringSender
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder(1024);

        public UInt32 LastErrorCode { get; private set; }

        public String LastErrorString { get; private set; }

        public Boolean TrySendString(String command) => this.TrySendString(command, out _);

        public Boolean TrySendString(String command, out String response)
        {
            Trace.TraceInformation($"Calling mciSendString({command})");

            this.LastErrorString = String.Empty;

            this.LastErrorCode = MciSendString(command, this._stringBuilder, this._stringBuilder?.Capacity ?? 0, IntPtr.Zero);
            if (0 == this.LastErrorCode)
            {
                response = this._stringBuilder.ToString();

                Trace.TraceInformation($"Call succeeded: '{response}'");
                return true;
            }

            response = null;

            Trace.TraceWarning($"mciSendString failed with error {this.LastErrorCode & 0xFFFF} (0x{this.LastErrorCode:X8})");

            if (MciGetErrorString(this.LastErrorCode, this._stringBuilder, this._stringBuilder.Capacity))
            {
                this.LastErrorString = this._stringBuilder.ToString();

                Trace.TraceWarning($"Error string: '{this.LastErrorString}'");
            }
            else
            {
                Trace.TraceWarning($"The error code is not known");
            }

            return false;
        }

        [DllImport("winmm.dll", ExactSpelling = true, EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi)]
        private static extern UInt32 MciSendString(String lpszCommand, StringBuilder lpszReturnString, Int32 cchReturn, IntPtr hwndCallback);

        [DllImport("winmm.dll", ExactSpelling = true, EntryPoint = "mciGetErrorStringA", CharSet = CharSet.Ansi)]
        private static extern Boolean MciGetErrorString(UInt32 fdwError, StringBuilder lpszErrorText, Int32 cchErrorText);
    }
}
