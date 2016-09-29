using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SlabBkp
{
    static class FileUtil
    {

        // Essa função verifica os drives disponíveis
        // e chama a função para extrair o conteúdo da APM, caso
        // ela exista. 
        //
        // Os drives procurados são de D - Z
        public static void copyFilesFromUsb()
        {
            string Mpfolder = @"C:\\logs_gravados"; //caminho da pasta no C
            char[] Unidade = new Char[23] {'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
                                           'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
                                           'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            // ve se a pasta existe se não cria a pasta
            if (!System.IO.Directory.Exists(Mpfolder))
            {
                System.IO.Directory.CreateDirectory(Mpfolder); //cria pasta principal no C
                DirectoryInfo diretorio = Directory.CreateDirectory(@"C:\\logs_gravados"); // pega info da pasta
                diretorio.Attributes = FileAttributes.Hidden; // depois deixa ela oculta
                Console.WriteLine("pasta criada");
            }
            else Console.WriteLine("pasta já existe");


            for (int i = 0; i < 23; i++)
            {
                Console.WriteLine("{0}", Unidade[i]);
                string Usbfolder = String.Format("{0}:\\APM", Unidade[i]);

                if (System.IO.Directory.Exists(Usbfolder))
                {
                    string file = String.Format("{0}\\LOGS\\1.BIN", Usbfolder);
                    FileInfo file_info = new FileInfo(file);
                    string Criado = file_info.CreationTime.ToString("dd_MM_yyyy-HH_mm_ss"); //pega a data de criação do arquivo

                    Console.WriteLine("pasta existe e vai ser copiada");

                    string Newfolder = String.Format("C:\\logs_gravados\\{0}", Criado);
                    Console.WriteLine("{0}", Criado);
                    if (!System.IO.Directory.Exists(Newfolder))
                    {
                        System.IO.Directory.CreateDirectory(Newfolder); //cria a nova pasta no C
                        FileUtil.DirectoryCopy(Usbfolder, Newfolder, true); // chama a funcao DirectoryCopy
                    }
                    Console.WriteLine("{0}", Newfolder);
                }
                Console.WriteLine("{0}", Usbfolder);
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
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
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
