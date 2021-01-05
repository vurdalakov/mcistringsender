namespace Vurdalakov.MciStringSender
{
    using System;

    public sealed class MciPlayer : IDisposable
    {
        private readonly MciStringSender _mciStringSender = new MciStringSender();

        private readonly String _alias = $"Vurdalakov.MciPlayer.{new Random().Next(100000, 999999)}";

        public void Dispose() => this.Close();

        public Boolean Open(String fileName) => this._mciStringSender.TrySendString($"open \"{fileName}\" alias {this._alias}");

        public Boolean Close() => String.IsNullOrEmpty(this.GetStatus()) || this._mciStringSender.TrySendString($"close {this._alias}");

        public String GetStatus() => this._mciStringSender.TrySendString($"status {this._alias} mode", out var status) ? status : null;

        public Boolean Play() => this._mciStringSender.TrySendString($"play {this._alias} from 0");

        public Boolean Stop() => this._mciStringSender.TrySendString($"stop {this._alias}");

        public Boolean Pause() => this._mciStringSender.TrySendString($"pause {this._alias}");

        public Boolean Resume() => this._mciStringSender.TrySendString($"resume {this._alias}");

        public Boolean Rewind() => this._mciStringSender.TrySendString($"seek {this._alias} to start");
    }
}
