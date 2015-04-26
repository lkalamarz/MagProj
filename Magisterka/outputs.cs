using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace Magisterka
{
    class outputs
    {
        SerialPort port;
        Encoding utf = Encoding.UTF8;
        private int adress;
        byte[] bufor;
        Timer timer;
        byte bffor;
        byte delay = 32;
        
        /// <summary>
        /// LED Diodes block
        /// </summary>

        public outputs(SerialPort serial, int adress,Timer timer)
        { this.port = serial;
        this.adress = adress;
        this.timer = timer;
        }

        public void Delay(int count)
        {
            while (count > 0)
                count--;
        }

        public void output_set_array(byte value)
        { 
        /*int adressDifference = adres - adress;
            string frame ="";
            
                for (int i = 0; i < maxRange - minRange; i++)
                {
                    frame = "1" + adressDifference.ToString() + (i+minRange).ToString();
                    if (values[i] == 1)
                    { frame += "14"; }
                    else
                    { frame += "04"; }
                    port.Write(frame);

                    while (port.BytesToRead < 0)
                    { }

                    int bytesAmount = port.BytesToRead;
                    if (bytesAmount > 0)
                    {
                        bufor = new byte[bytesAmount];
                        port.Read(bufor, 0, bytesAmount);


                        // bffor = (byte)((int)bufor[0] - (int)delay);
                    }
                }*/
            byte[] buffer = { 51, 48, 48, 0, 52 };
            buffer[3] = value;
            port.Write(buffer, 0 ,5);
        }

        public void output_set(int adres, int pin, bool value)
        {
           // timer.Enabled = false;
            int adressDifference = adres - adress;
            string frame ="";
            if ((adressDifference >= 0 && adressDifference <= 3) || (adressDifference == 4 && (pin == 0 || pin == 1)) || adressDifference == 5)
            {
                frame = "1" + adressDifference.ToString() + pin.ToString();
                if (value == true)
                { frame += "14"; }
                else
                { frame += "04"; }
                port.Write(frame);

                while (port.BytesToRead < 0)
                { }

               /* int bytesAmount = port.BytesToRead;
                if (bytesAmount > 0)
                {
                    bufor = new byte[bytesAmount];
                    port.Read(bufor, 0, bytesAmount);

                   
                    // bffor = (byte)((int)bufor[0] - (int)delay);
                }*/
           }

            //timer.Enabled = true;
        }


        public int outputs_read(int adres)
        {
            int adressDifference = adres - adress;
            if (adressDifference == 0)
            {
                port.Write("20434");
            }
            else if (adressDifference == 1)
            {

                port.Write("21434");

            }
            else if (adressDifference == 2)
            {

                port.Write("22434");

            }
            else if (adressDifference == 3)
            {

                port.Write("23034");

            }
            else if (adressDifference == 4)
            {
                port.Write("24334");
            }


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
                return (int)bufor[0];
               // bffor = (byte)((int)bufor[0] - (int)delay);
            }
            return 0;
        }


        int[] set_array(int[] destination, int wynik, short minRange, short maxRange)
        {
            for (int i = 7; i >= 0; i--)
            {
                if (wynik - (int)Math.Pow(2, i) >= 0)
                {
                    if (i >= minRange && i <= maxRange)
                        destination[i] = 1;
                    wynik -= (int)Math.Pow(2, i);
                }
                else
                    destination[i] = 0;
            }

            return destination;
        }
        /// <summary>
        /// Proessing obtained result to readable for user form for array
        /// </summary>
        /// <param name="wynik">Result obtained from UART communication</param>
        /// <param name="pin">Desired pin to read</param>
        /// <returns>Return 0 or 1 depending of state of pin</returns>

        int set_value(int wynik, short pin)
        {
           
            
                if (wynik - (int)Math.Pow(2, pin) >= 0)
                {
                    
                        return 1;

                }
                else
                    return 0;
           
        }

        byte ConvertToString(int[] value)
        {
            int result = 0;
            for (int i = 7; i > -1; i--)
            { 
            result+=value[i] * (int)Math.Pow(2,i);
            }
            Encoding utf = Encoding.UTF8;
            byte[] x = { (byte)result };
            return x[0];
            
        }
        ///
        /// LED Block
        /// 

        public int[] LEDsRead()
        {
            int[] result = new int[7];
            result = set_array(result, outputs_read(this.adress + 5), 0, 7);
            return result;
        }

       public int LEDsRead(int i)
        {
            return set_value(outputs_read(this.adress + 5), (short)i);
        }

      public void LEDSet(int[] values)
        { output_set_array(ConvertToString(values)); }

       public void LEDSet(int i, int val)
        { }

        ///
        ///Buzzer block
        ///

       public int BuzzerRead()
        {
            return set_value(outputs_read(this.adress + 4), 1);
        }

       public void BuzzerSet(int i)
        {
            if (i == 0)
            {
                output_set(this.adress + 4, 1, false);
            }
            else if (i == 1)
            { output_set(this.adress + 4, 1, true); }
            else
            { }

        }

        ///
        ///DAC Block
        ///

        int[] DACRead()
        {
            int[] result = new int[7];
            result = set_array(result, outputs_read(this.adress + 3), 0, 7);
            return result;
        }

        int DACRead(int i)
        {

            return set_value(outputs_read(this.adress + 3), (short)i);
        }

        void DACSet(int[] values)
        { }

        void DACSet(int i, int value)
        { 
        }

        ///
        ///7Segment Displat Block
        ///

       int[] Display7_ARead()
       {
            int[] result = new int[4];
            result = set_array(result, outputs_read(this.adress + 2), 0, 3);
            return result;
       }

       int Display7_ARead(int i)
       {
           if (i>= 0 && i<=3)
           return set_value(outputs_read(this.adress + 2), (short)i);

           return -1;
       }

       int[] Display7_BRead()
       {
           int[] result = new int[4];
           result = set_array(result, outputs_read(this.adress + 2), 4, 7);
           return result;
       }

       int Display7_BRead(int i)
       {
           if (i >= 4 && i <= 7)
               return set_value(outputs_read(this.adress + 2), (short)i);

           return -1;
       }

       int[] Display7_cRead()
       {
           int[] result = new int[4];
           result = set_array(result, outputs_read(this.adress + 1), 0, 3);
           return result;
       }

       int Display7_CRead(int i)
       {
           if (i >= 0 && i <= 3)
               return set_value(outputs_read(this.adress + 1), (short)i);

           return -1;
       }

       int[] Display7_DRead()
       {
           int[] result = new int[4];
           result = set_array(result, outputs_read(this.adress + 1), 4, 7);
           return result;
       }

       int Display7_DRead(int i)
       {
           if (i >= 4 && i <= 7)
               return set_value(outputs_read(this.adress + 1), (short)i);

           return -1;
       }

       void Display7_ASet(int[] i)
       {
           
       }

       void Display7_ASet(int i, int val)
       {
           if (i >= 0 && i <= 3)
           {
               if (val == 0)
               {
                   output_set(this.adress + 2, i, false);
               }
               else if (val == 1)
               { output_set(this.adress + 2, i, true); }
               else
               { }
           }
           else
           { }
       }

       void Display7_BSet(int[] i)
       {
           
       }

       void Display7_BSet(int i, int val)
       {
           if (i >= 4 && i <= 7)
           {
               if (val == 0)
               {
                   output_set(this.adress + 2, i, false);
               }
               else if (val == 1)
               { output_set(this.adress + 2, i, true); }
               else
               { }
           }
           else
           { }
       }
       
        void Display7_CSet(int[] i )
       {
           
       }

       void Display7_CSet(int i, int val)
       {
           if (i >= 0 && i <= 3)
           {
               if (val == 0)
               {
                   output_set(this.adress + 1, i, false);
               }
               else if (val == 1)
               { output_set(this.adress + 1, i, true); }
               else
               { }
           }
           else
           { }
       }

       void Display7_DSet(int[] i)
       {
           
       }

       void Display7_DSet(int i, int val)
       {
           if (i >= 4 && i <= 7)
           {
               if (val == 0)
               {
                   output_set(this.adress + 1, i, false);
               }
               else if (val == 1)
               { output_set(this.adress + 1, i, true); }
               else
               { }
           }
           else
           { }
       }

        ///
        /// KeyboardBlock
        /// 


       int[] KeyboardRead()
       {
           int[] result = new int[4];
           result = set_array(result, outputs_read(this.adress), 0, 3);
           return result;
       }

       int KeyboardRead(int i)
       {
           if (i>=0 && i<=3)
           return set_value(outputs_read(this.adress), (short)i);

           return -1;
       }

       void KeyboardSet(int[] i)
       { }

       void KeyboardSet(int i, int val)
       {
           if (i >= 0 && i <= 3)
           {
               if (val == 0)
               {
                   output_set(this.adress, i, false);
               }
               else if (val == 1)
               { output_set(this.adress, i, true); }
               else
               { }
           }
           else
           { }
       }

        ///
        ///Motor Block
        ///

       int[] MotorRead()
       { 
       int[] result = new int[4];
           result = set_array(result, outputs_read(this.adress), 4, 7);
           return result;
       }

       int MotorRead(int i)
       { 
       if (i>=4 && i<=7)
           return set_value(outputs_read(this.adress), (short)i);

           return -1;
       }

       void MotorSet(int[] i)
        {}

       void MotorSet(int i, int val)
        {
       if (i >= 4 && i <= 7)
           {
               if (val == 0)
               {
                   output_set(this.adress, i, false);
               }
               else if (val == 1)
               { output_set(this.adress, i, true); }
               else
               { }
           }
           else
           { }
       }
    }
}
