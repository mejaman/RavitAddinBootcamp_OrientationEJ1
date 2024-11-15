using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;         //*****Conflict with Application command*******
using System.IO;

namespace RavitAddinBootcamp_OrientationEJ1
{
    [Transaction(TransactionMode.Manual)]
    public class cmdDeltBKU : IExternalCommand
    {

        public Result Execute(
            ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            int counter = 0;
            string logPath = "";
            List<string> deletedFileLog = new List<string>();
            deletedFileLog.Add("The Following backup files have been deleted:");

            FolderBrowserDialog selectFolder = new FolderBrowserDialog();
            selectFolder.ShowNewFolderButton = false;

            // open folder dialog and only run code if a folder is selected

            if (selectFolder.ShowDialog() == DialogResult.OK)
            {

                // get the selected folder path
                string directory = selectFolder.SelectedPath;

                //with system.IO get all files from selected folder

                // string[] files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
                string[] files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);


                foreach (string file in files)
                {

                    if (Path.GetExtension(file) == ".rvt" || Path.GetExtension(file) == ".rfa")
                    {
                        // get the last 9 characters of filename to check if backup
                        string checkString = file.Substring(file.Length - 9, 9);
                        if (checkString.Contains(".00") == true)
                        {
                            //add filename to list
                            deletedFileLog.Add(file);

                            //delete file
                            File.Delete(file);
                            counter++;
                        }
                    }


                }

                // output log file
                if (counter > 0)
                {
                    logPath = WriteListToTxt(deletedFileLog, directory);

                }


            }

            // alert user
            TaskDialog td = new TaskDialog("Complete");
            td.MainInstruction = "Deleted " + counter.ToString() + " backup files.";
            td.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "click to Log file - eja");
            td.CommonButtons = TaskDialogCommonButtons.Ok;

            TaskDialogResult result = td.Show();

            if (result == TaskDialogResult.CommandLink1)
            {

                Process.Start(logPath);
            }


            return Result.Succeeded;



        }

        internal string WriteListToTxt(List<string> stringList, string filePath)
        {

            string fileName = "_Delete Backup Files.text";
            string fullPath = filePath + @"\" + fileName;
            File.WriteAllLines(fullPath, stringList);
            return fullPath;

        }

    }
}
