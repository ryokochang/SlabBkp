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

            // Fica eternamente executando com intervalo de 10 segundos
            while (1 == 1)
            {
                FileUtil.copyFilesFromUsb();
                System.Threading.Thread.Sleep(10000);
            }
        }


    }
}
