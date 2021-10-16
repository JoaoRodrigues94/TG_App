using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Windows.Storage;
using Windows.Storage.Pickers;
using Xamarin.Forms.Platform.UWP;
using TG_App.View;
using TG_App.UWP;
using Windows.System;

[assembly: Dependency(typeof(SaveWindows))]

namespace TG_App.UWP
{
  class SaveWindows : ISave
  {
    public async Task SaveAndView(string filename, string contentType, MemoryStream stream)
    {
      //save the stream into the file. 
      if (Device.Idiom != TargetIdiom.Desktop)
      {
        StorageFolder local = ApplicationData.Current.LocalFolder;
        StorageFile outFile = await local.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
        using (Stream outStream = await outFile.OpenStreamForWriteAsync())
        {
          outStream.Write(stream.ToArray(), 0, (int)stream.Length);
        }
        if (contentType != "application/html")
          await Launcher.LaunchFileAsync(outFile);
      }
      else
      {
        StorageFile storageFile = null;
        FileSavePicker savePicker = new FileSavePicker();
        savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
        savePicker.SuggestedFileName = filename;
        contentType = "application/pdf";
        switch (contentType)
        {
          case "application/vnd.openxmlformats-officedocument.presentationml.presentation":
            savePicker.FileTypeChoices.Add("PowerPoint Presentation", new List<string>() { ".pptx", });
            break;

          case "application/msexcel":
            savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx", });
            break;

          case "application/msword":
            savePicker.FileTypeChoices.Add("Word Document", new List<string>() { ".docx" });
            break;

          case "application/pdf":
            savePicker.FileTypeChoices.Add("Adobe PDF Document", new List<string>() { ".pdf" });
            break;
          case "application/html":
            savePicker.FileTypeChoices.Add("HTML Files", new List<string>() { ".html" });
            break;
        }
        storageFile = await savePicker.PickSaveFileAsync();

        using (Stream outStream = await storageFile.OpenStreamForWriteAsync())
        {
          outStream.Write(stream.ToArray(), 0, (int)stream.Length);
        }

        //Invoke the saved file for Viewing.
        await Launcher.LaunchFileAsync(storageFile);
      }
    }
  }
}
