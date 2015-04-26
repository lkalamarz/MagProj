using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace Magisterka
{
    class inputs
    {

        SerialPort port;
        private int adress;
        Timer timer;
        byte[] bufor;
        byte bffor;
        byte delay = 32;
        
        /// <summary>
        /// LED Diodes block
        /// </summary>

        public inputs(SerialPort serial, int adress,Timer timer)
        { this.port = serial; 
            this.adress = adress;
            this.timer = timer;
        }

    
        public void Delay(int count)
        {
            while (count > 0)
                count--;
        }

        public int pin_read(int adres, int pin)
        {
            int wynik = inputs_read(adres);
            if (wynik - (int)Math.Pow(2, pin) > 0)
                return 1;
            else
                return 0;
        }


        public int inputs_read(int adres)
        {
            int adressDifference = adres - adress;
            if (adressDifference == 0)
            {
                    port.Write("00434");
            }
            else if (adressDifference == 1)
            {
            
                    port.Write("01434");

            }
            else if (adressDifference == 2)
            {

                    port.Write("02434");

            }
            else if (adressDifference == 3)
            {

                    port.Write("03034");

            }
            else if (adressDifference == 4)
            {
 port.Write("04334"); }

 
            else
            {
                return -1;
            }

            while (port.BytesToRead < 0)
            { }

          int bytesAmount = port.BytesToRead;
          if (bytesAmount > 0)
          {
              bufor = new byte[bytesAmount];
              port.Read(bufor, 0, bytesAmount);
              bffor = (byte)((int)bufor[0] );
          }
          return (int)bffor;
        }

        public int input_read(int adres, int pin)
        {
           // Delay(100000000);
            //timer.Enabled = false;
        int adressDifference = adres - adress;
    /*    if (adressDifference == 0)
        {
            if (pin == 4)
            {
                    port.Write("01234");

            }
            else if (pin == 5)
            { 
                port.Write("gowno"); 
            }
            else if (pin == 6)
            { }
            else if (pin == 7)
            { }
            else 
            { 
              //  System.Windows.Forms.MessageBox.Show("Wrong adress"); 
            }

        }*/

      /* timer.Enabled = true;

            int bytesAmount = port.BytesToRead;
            if (bytesAmount > 0)
            {
                bufor = new byte[bytesAmount];
                port.Read(bufor, 0, bytesAmount);
               
                    
            }
           if (bufor[0] == 0)
            {
                timer.Enabled = true;
                return 0;
            }
            else if (bufor[0] == 1)
            {
                timer.Enabled = true;
                return 1;
            }
            else
            {
                timer.Enabled = true;
                System.Windows.Forms.MessageBox.Show("Wrong data obtained");
                return -1;
            }*/
           
            
            if (adressDifference == 0)
            {
                if (pin == 4)
                {
                    port.Write("00434");

                }
                else if (pin == 5)
                {
                    port.Write("00534");
                }
                else if (pin == 6)
                { port.Write("00634"); }
                else if (pin == 7)
                { port.Write("00734"); }
                else
                {
                    return -1;
                }

            }
            else if (adressDifference == 1)
            {
                if (pin == 0)
                {
                    port.Write("01034");

                }
                else if (pin == 1)
                {
                    port.Write("01134");
                }
                else if (pin == 2)
                { port.Write("01234"); }
                else if (pin == 3)
                { port.Write("01334"); }
                else if (pin == 4)
                {
                    port.Write("01434");

                }
                else if (pin == 5)
                {
                    port.Write("01534");
                }
                else if (pin == 6)
                { port.Write("01634"); }
                else if (pin == 7)
                { port.Write("01734"); }
                else
                {
                    return -1;
                }
            }
            else if (adressDifference == 2)
            {
                if (pin == 0)
                {
                    port.Write("02034");

                }
                else if (pin == 1)
                {
                    port.Write("02134");
                }
                else if (pin == 2)
                { port.Write("02234"); }
                else if (pin == 3)
                { port.Write("02334"); }
                else if (pin == 4)
                {
                    port.Write("02434");

                }
                else if (pin == 5)
                {
                    port.Write("02534");
                }
                else if (pin == 6)
                { port.Write("02634"); }
                else if (pin == 7)
                { port.Write("02734"); }
                else
                {
                    return -1;
                }
            }
            else if (adressDifference == 3)
            {
                if (pin == 0)
                {
                    port.Write("03034");

                }
                else if (pin == 1)
                {
                    port.Write("03134");
                }
                
                else
                {
                    return -1;
                }
            }
            else if (adressDifference == 4)
            {
                if (pin == 0)
                {
                    port.Write("04034");

                }
                else if (pin == 1)
                {
                    port.Write("04134");
                }
                else if (pin == 2)
                { port.Write("04234"); }
                else if (pin == 3)
                { port.Write("04334"); }
                else
                {
                    return -1;
                }
            }
            else 
            {
                return -1;
            }

            //timer.Enabled = true;
        //    byte delay = Convert.ToByte(32);
            while (port.BytesToRead < 0)
            { }
            //Delay(270000);
          int bytesAmount = port.BytesToRead;
            if (bytesAmount > 0)
            {
                bufor = new byte[bytesAmount];
                port.Read(bufor, 0, bytesAmount);
                bffor =(byte)( (int)bufor[0] - (int)delay);
               // MessageBox.Show(Convert.ToString(bufor));


            }

            if ((int)bffor - (int)Math.Pow(2,pin) > 0)
                return 1;
            else return 0;


         /*   if (bufor != null)
            {
                if (bufor[bufor.Length-1] == 0)
                {
                    // timer.Enabled = true;
                    return 0;
                }
                else if (bufor[bufor.Length-1] == 1)
                {
                    //timer.Enabled = true;
                    return 1;
                }
                else
                {
                    //timer.Enabled = true;
                  //  System.Windows.Forms.MessageBox.Show("Wrong data obtained");
                    return -1;
                }
            }*/
     
        }
        
        

        int[] set_array(int[] destination, int wynik, short minRange, short maxRange)
        {
            for (int i = 7; i >= 0; i--)
            {
                if (wynik - (int)Math.Pow(2, i) >= 0)
                {
                    if (i>=minRange && i<=maxRange)
                    destination[i] = 1;
                    wynik -= (int)Math.Pow(2, i);
                }
                else
                    destination[i] = 0;
            }

            return destination;
        }
        /// <summary>
        /// Processing obtained result to readable form for user from array
        /// </summary>
        /// <param name="wynik">Result obtained from UART communication</param>
        /// <param name="pin">Desired pin to read</param>
        /// <returns>Return 0 or 1 depending of state of pin</returns>

        int set_value(int wynik, short pin)
        {
            for (int i = 7; i >= pin; i--)
            {
                if (wynik - (int)Math.Pow(2, i) >= 0)
                {
                    if (i ==pin)
                        return 1;
                    
                }
                else
                    return 0;
            }
            return 0;
        }

        /// <summary>
        ///  DIP Switch block
        /// </summary>

        public int DIPSwitchCheck(int pin)
        {
            if (pin >= 0 && pin <= 7)
            {
                return set_value(input_read(this.adress + 1, pin),(short)pin);
               
            }
            else
                return -1;
        }

        public int[] DIPSwitchCheck()
        { 
            int[] result = new int[7];
            result = set_array(result,input_read(this.adress,inputs_read(this.adress+1)),0,7);
            return result;
        }

        /// 
        /// ADC Block
        /// 
        int[] ADCvalue()
        {
            int[] result = new int[8];
            result = set_array(result, input_read(this.adress, inputs_read(this.adress + 2)), 0, 7);
            return result;
        }

        int ADCstatus()
        { 
        return set_value(inputs_read(this.adress+4),1);
        }

        ///
        ///SwitchButtons Block
        ///

        int[] SwitchButtons()
        { 
            int[] result = new int[2];
            result = set_array(result,input_read(this.adress,inputs_read(this.adress+3)),0,1);
            return result;
        }

        int SwitchButtons(int pin)
        {
            if (pin == 0 || pin == 1)
                return set_value(inputs_read(this.adress + 3), (short)pin);

            else
                return -1;
        }

        ///
        ///Comparator Block
        ///

        public int Comparator()
        {
            return set_value(inputs_read(this.adress + 4), 2);
        }

        ///
        ///Oscilator Block
        ///

        int Oscilator()
        {
            return set_value(inputs_read(this.adress + 4), 3);
        }

        ///
        ///Rotor Block
        ///

        int RotorPosition()
        {
            return set_value(inputs_read(this.adress + 4), 0);
        }

        ///
        ///Keyboard Block
        ///

        int[] KeyboardInput()
        {
            int[] result = new int[4];
            result = set_array(result, input_read(this.adress, inputs_read(this.adress )), 4, 7);
            return result;
        }

        int KeyboardInput(int i)
        {
            if (i > 3 & i < 8)
                return set_value(inputs_read(this.adress), (short)i);

            else
                return -1;
        }
    }
}
