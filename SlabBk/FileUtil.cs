using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SlabBkp
{
    class FileUtil
    {

        // ve se a pasta existe se não cria a pasta oculta e retorna o diretorio.
        public string newHiddenFolder (string sourceDir)
            {
                if (!System.IO.Directory.Exists(sourceDir))
                {
                System.IO.Directory.CreateDirectory(sourceDir);
                DirectoryInfo diretorio = Directory.CreateDirectory(sourceDir);
                diretorio.Attributes = FileAttributes.Hidden;
                return sourceDir;
                }
            return sourceDir;

        }

        //função que verificar se o diretorio existem em todas as unidades e retorna o a letra da unidade caso o diretorio exista, caso não exista retorna A <- unidade que não existe.
        public char findUnitFolder(string sourceDir)
        {
            char[] Unidade = new Char[23] {'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
                                           'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
                                           'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            for (int i = 0; i < 23; i++)
            {
                string Usbfolder = String.Format("{0}:\\{1}", Unidade[i], sourceDir);
                if (System.IO.Directory.Exists(Usbfolder))
                {
                    return Unidade[i];
                }

            }
            return 'A';
        }

        // Essa função verifica os drives disponíveis
        // e chama a função para extrair o conteúdo da APM, caso
        // ela exista. 
        //
        // Os drives procurados são de D - Z
        public void copyFilesFromUsb(char unit, string folderName, string newSource)
        {
            string rootFolder = string.Format("{0}:\\{1}", unit, folderName);
            Console.WriteLine("{0}", rootFolder);
            string file = string.Format("{0}:\\{1}\\LOGS\\1.BIN", unit, folderName);
            FileInfo file_info = new FileInfo(file);
            string dataCriacao = file_info.CreationTime.ToString("dd_MM_yyyy-HH_mm_ss");
            string novaPasta = string.Format("{0}\\{1}", newSource, dataCriacao);
            Console.WriteLine("{0}", novaPasta);
            if (!System.IO.Directory.Exists(novaPasta))
            {
                System.IO.Directory.CreateDirectory(novaPasta);
                DirectoryCopy(rootFolder, novaPasta, true);
            }
        }
        
        // Esta função copia todos os arquivos e caso for necessário  
        // também copia as pastas e sub pastas de um diretório
        //para um destino
        //
        // Parâmetros: 
        // @sourceDirName - Diretório destino de onde os arquivos serão copiados
        // @destDirName - Diretório destino para onde os arquivo vão
        // @copySubDirs - Booleana true or false para copiar os subdiretorios
        public void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            var dir = new DirectoryInfo(sourceDirName);
            var dirs = dir.GetDirectories();

            // Se o diretório de origem não existe, cria uma exceção.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // Se o diretório de destino não existir, então cria a pasta.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }


            // Obter o conteúdo do diretório de arquivos para copiar.
            var files = dir.GetFiles();

            foreach (var file in files)
            {

                // cria o novo diretório para a copia do arquivo
                var temppath = Path.Combine(destDirName, file.Name);

                // Copia o arquivo
                file.CopyTo(temppath, true);
            }


            // se copySubDirs for verdadeiro, copia os subdiretorios.
            if (!copySubDirs) return;

            foreach (var subdir in dirs)
            {

                // cria os subdiretorios
                var temppath = Path.Combine(destDirName, subdir.Name);


                //copiar os sub diretorios
                DirectoryCopy(subdir.FullName, temppath, copySubDirs);
            }
        }
    }
}
