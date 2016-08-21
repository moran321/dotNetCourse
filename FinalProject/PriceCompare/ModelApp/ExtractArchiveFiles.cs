using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelApp
{
    public class ExtractArchiveFiles
    {
        public void ExtractCompressedFiles(string zipPath, string[] zipFiles)
        {
            if (zipFiles.Any())
            {
                string type = Path.GetExtension(zipFiles[0]);
                if (type.Equals(".zip"))
                {
                    ExtractZip(zipPath, zipFiles);
                }
                else if (type.Equals(".gz"))
                {
                    ExtractGz(zipPath, zipFiles);
                }
            }
        }
        /*---------------------------------*/

        private void ExtractGz(string zipPath, string[] zipFiles)
        {
            foreach (string zip in zipFiles)
            {
                MemoryStream decodedStream = new MemoryStream();
                byte[] buffer = new byte[1024];
                string xmlOutName = Path.GetFileNameWithoutExtension(zip) + ".xml";
                Stream stream = new FileStream(zip, FileMode.Open, FileAccess.Read, FileShare.Read);

                using (Stream inGzipStream = new GZipStream(stream, CompressionMode.Decompress))
                {
                    int bytesRead;
                    while ((bytesRead = inGzipStream.Read(buffer, 0, buffer.Length)) > 0)
                        decodedStream.Write(buffer, 0, bytesRead);
                }
                var pathtofile = Path.Combine(zipPath, xmlOutName);

                if (File.Exists(pathtofile))
                {
                    File.Delete(pathtofile);
                }
                File.WriteAllBytes(pathtofile, decodedStream.ToArray()); 

            }
        }
        /*---------------------------------*/

        
        private void ExtractZip(string zipPath, string[] zipFiles)
        {
            //delete old
            string[] xmlOldFiles = Directory.GetFiles(zipPath, "Stores*.xml");
            foreach (string xmlFile in xmlOldFiles)
            {
                File.Delete(xmlFile);
            }
            //////

            foreach (string zip in zipFiles)
            {
                ZipFile.ExtractToDirectory(zip, zipPath);
            }

        }
        /*---------------------------------*/

    }
}
