using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Services
{
    public interface IDialogService
    {

        /// <summary>
        /// 显示msg
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        void ShowMsgBox(string title, string msg);

        /// <summary>
        /// 显示dialog
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool? ShowConfirmBox(string title, string msg);
    }
}
