using System;
using System.Collections.Generic;
using System.Text;

namespace TG_App.Model
{
  class NotificationEventArgs: EventArgs
  {
    public string Title { get; set; }
    public string Message { get; set; }
  }
}
