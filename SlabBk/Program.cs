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
                string pasta = string.Format("APM"); // pasta que vai ser verificada e copiada
                string log_base = fileManager.newHiddenFolder("C:\\logs_de_Base"); // pasta de backup de log de base
                string log_sd = fileManager.newHiddenFolder("C:\\logs_gravados1"); //pasta de backpup de log do SD
            
            // Fica eternamente executando com intervalo de 5 segundos
            while (1 == 1)
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
