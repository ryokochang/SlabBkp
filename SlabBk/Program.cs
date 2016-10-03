/* usado para copiar a pasta APM dentro de algum dispositivo movel (pendrive, cartão sd, etc...)
 * confere se existe a pasta APM em todas as unidades a cada 10 segundos, 
 * verifica a data de criação do arquivo VALOR DA UNIDADE:/APM/LOGS/1.BIN e cria uma pasta com a data de criação do arquivo
 * em C:/logs_gravados/DATA DE CRIAÇÂO.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SlabBkp
{
    class Program
    {
        static void Main(string[] args)
        {
            FileUtil fileManager = new FileUtil();
            // criação das pastas e configuração
            string pasta = "APM"; // pasta que vai ser verificada e copiada
            string log_base = fileManager.newHiddenFolder(@"C:\logs de Base"); // pasta de backup de log de base
            string log_sd = fileManager.newHiddenFolder(@"C:\logs gravados"); //pasta de backpup de log do SD
            // pega o caminho do usuario e transforma em uma string
            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                path = Directory.GetParent(path).ToString();
            }
            string log_folder = string.Format(@"{0}\Documents\Mission Planner\logs", path);
            // função que mantem o backup da pasta log atualizado
            // recebe o diretório da pasta log do mission planner e recebe a pasta de back dos logs
            fileManager.watch(log_folder,log_base);
            // Fica eternamente executando com intervalo de 5 segundos
            while (true)
            {
                char unit = fileManager.findUnitFolder(pasta);
                if (unit != 'A')
                {
                    fileManager.copyFilesFromUsb(unit, "APM", log_sd);
                }
                System.Threading.Thread.Sleep(5000);
            }
        }
    }
}
