﻿using System.Windows.Controls;

namespace Tools.Site
{
    public class API
    {
        public WebBrowser WebBrower;

        public void LoadSesson(string username, string password)
        {
            WebBrower = new WebBrowser();
            WebBrower.Navigate("https://www.papelariameiomundo.com/Login");
        }
    }
}
