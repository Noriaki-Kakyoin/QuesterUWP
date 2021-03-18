﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Quester.Helper
{
    class IOHelper
    {
        public static string GetUserHomeDir()
        {
            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                path = Directory.GetParent(path).ToString();
            }
            return path;
        }

        public static string GetDefaultProjectDir()
        {
            string exe = Path.Combine(
                ApplicationData.Current.LocalFolder.Path, "QuesterProjects");
            return exe;
        }

        public static string FormatProjectPath(string projectName)
        {
            if(!String.IsNullOrEmpty(projectName))
                return Path.Combine(GetDefaultProjectDir(), projectName);
            return "";
        }

        public static string FormatProjectPath(string projectName, string customPath)
        {
            if (!String.IsNullOrEmpty(projectName) && !String.IsNullOrEmpty(customPath))
                return Path.Combine(customPath, projectName);

            return customPath;
        }

        // Default projects dir
        public static bool IsProjectAvailable(string projectDir)
        {
            if (!String.IsNullOrWhiteSpace(projectDir))
            {
                return !Directory.Exists(projectDir);
            }

            return false;
        }

        public async static void EnsureProjectStructure()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            
            StorageFolder folder = await storageFolder.CreateFolderAsync("QuesterProject",
                CreationCollisionOption.OpenIfExists);
        }

        public async static Task<bool> CreateFile(string path)
        {
            EnsureProjectStructure();

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

            try
            {
                StorageFile file = await storageFolder.CreateFileAsync(path, CreationCollisionOption.FailIfExists);
            }
            catch (Exception e)
            {
                // Folder exist
                return false;
            }

            return true;
        }
    }
}
