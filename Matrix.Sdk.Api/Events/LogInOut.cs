﻿using System;

namespace Matrix.Sdk.Api.Events
{
    public partial class Events
    {
        public class LoginEventArgs : EventArgs
        {
            public string AccessToken;
            public string DeviceID;
            public string UserID;
        }

        public delegate void LoginFailDelegate(object sender, ErrorEventArgs e);
        public delegate void LoginDelegate(object sender, LoginEventArgs e);
        public delegate void LogoutDelegate(object sender, LoginEventArgs e);

        public event LoginFailDelegate LoginFailEvent;
        public event LoginDelegate LoginEvent;
        public event LogoutDelegate LogoutEvent;

        internal void FireLoginFailEvent(string message) => LoginFailEvent?.Invoke(this, new ErrorEventArgs() { Message = message });
        internal void FireLoginEvent(Responses.Session.LoginResponse resp) => LoginEvent?.Invoke(this, new LoginEventArgs() {
            AccessToken = resp.AccessToken,
            DeviceID = resp.DeviceID,
            UserID = resp.UserID
        });
        internal void FireLogoutEvent() => LogoutEvent?.Invoke(this, new LoginEventArgs() { });
    }
}
