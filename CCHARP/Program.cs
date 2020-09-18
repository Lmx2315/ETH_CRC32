using System;


namespace CCHARP
{
    class Program
    {
        static void Main(string[] args)
        {
            bool error = false;
            string text="";
            uint crc = 0;
            Console.WriteLine("---------");

            try
            {
                if (args.Length != 0)
                {
                   //  Console.WriteLine(args[0]);
                    text = System.IO.File.ReadAllText(args[0]);
                }
                else text = System.IO.File.ReadAllText("dump.txt");
            } catch
            {
                Console.WriteLine("нет файла!");
                error = true;
            }

            if (error==false)//если файл есть 
            {
                // Console.WriteLine(text);  
                crc= CRC_calc(text);
            }

            Console.WriteLine("--------------");
            Console.WriteLine("crc:"+crc.ToString("X"));
        }



        static uint CRC_calc (string a)
        {
           string [] words = a.Split (new char [] {' ','\r','\n'});
               int[] arr = new int[words.Length];
       
            uint code = 0;
            uint crc = 0xffffffff;
            uint z0 = 0;
            uint z1 = 0;
            uint z2 = 0;
            uint z3 = 0;
            uint k = 0;

           foreach (var item in words)
           {
               
                    try
                {
                    uint value = Convert.ToUInt32(item, 16);
                    if (value<256)
                    {
                        if (k == 0) z0 = value;
                        if (k == 1) z1 = value;
                        if (k == 2) z2 = value;
                        if (k == 3) z3 = value;

                      //  Console.Write(item + " ");
                      //  Console.WriteLine(value);
                      //  arr[i++] = value;
                        
                        if (k != 3) k++;
                        else
                        {
                            code = (z3 << 24) + (z2 << 16) + (z1 << 8) + (z0 << 0);
                            crc = crc ^ code;
                            Console.WriteLine((crc^0xffffffff).ToString("X"));
                            k = 0;
                        }
                    }
                    
                } catch
                {
                  //  Console.WriteLine("+");
                }
                    
         
           }

            return crc;
        }


    }
}
