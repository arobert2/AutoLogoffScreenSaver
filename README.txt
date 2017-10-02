Compiling from source:

When compiling from source you must rename your application to the .scr extension. You also must change the exe to an scr in the config filename. Create a folder called images at the root of the program and drop the logo you want to use in there. If the logo is not called org_logo.png you'll need to edit the Logoff_ScreenSaver.scr.config file to point to the correct file.

Using the release:

Copy the files to C:\Windows and select the screen saver from the Windows screen saver dialog box.

To configure the logout time edit the LogOffTimeInSeconds value in the Logoff_ScreenSaver.scr.config file.

To configure a logo you can overwrite the existing logo in the images folder or you can change the directory in the Logoff_ScreenSaver.scr.config file, but it's relative to the folder the application is running from.
