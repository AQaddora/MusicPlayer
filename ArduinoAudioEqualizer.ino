#include <LiquidCrystal.h>

  LiquidCrystal lcd(12,11,2,3,4,5,6,7,8,9);

  int incomingByte = 0;   // for incoming serial data

  String input;
  byte char01[8]
  {
    B00000,
    B00000,
    B00000,
    B00000,
    B00000,
    B00000,
    B00000,
    B11111
  };
    byte char02[8]
  {
    B00000,
    B00000,
    B00000,
    B00000,
    B00000,
    B00000,
    B11111,
    B11111
  };
    byte char03[8]
  {
    B00000,
    B00000,
    B00000,
    B00000,
    B00000,
    B11111,
    B11111,
    B11111
  };
    byte char04[8]
  {
    B00000,
    B00000,
    B00000,
    B00000,
    B11111,
    B11111,
    B11111,
    B11111
  };
    byte char05[8]
  {
    B00000,
    B00000,
    B00000,
    B11111,
    B11111,
    B11111,
    B11111,
    B11111
  };
    byte char06[8]
  {
    B00000,
    B00000,
    B11111,
    B11111,
    B11111,
    B11111,
    B11111,
    B11111
  };
    byte char07[8]
  {
    B00000,
    B11111,
    B11111,
    B11111,
    B11111,
    B11111,
    B11111,
    B11111
  };
      byte char08[8]
  {
    B11111,
    B11111,
    B11111,
    B11111,
    B11111,
    B11111,
    B11111,
    B11111
  };

int rcv = -1;
void setup() 
 {
  input = "";
  Serial.begin(9600); // opens serial port, sets data rate to 9600 bps
  lcd.begin(16,2);
  lcd.createChar(1,char01);
  lcd.createChar(2,char02);
  lcd.createChar(3,char03);
  lcd.createChar(4,char04);
  lcd.createChar(5,char05);
  lcd.createChar(6,char06);
  lcd.createChar(7,char07);
  lcd.createChar(8,char08);
  lcd.noCursor();
  lcd.setCursor(0,1);
 }

 int i = 0;
 int positionx = 0;
 
 void loop()
 {
     //send data only when you receive data:
    if (Serial.available() > 0) {
            incomingByte = Serial.read();
            if(incomingByte == '-')
            {
              lcd.setCursor(0,1);
              positionx = 0;
            }else
            {
              char c = incomingByte;
              i = c;
              if(i == 48)
              {
                lcd.setCursor(positionx, 0);
                lcd.print(" ");
                lcd.setCursor(positionx, 1);
                lcd.write(3);
              }else if(i == 49)
              {
                lcd.setCursor(positionx, 0);
                lcd.print(" ");
                lcd.setCursor(positionx, 1);
                lcd.write(6);
              }
              else if(i == 50)
              {
                lcd.setCursor(positionx, 0);
                lcd.print(" ");
                lcd.setCursor(positionx, 1);
                lcd.write(8);
                lcd.setCursor(positionx, 0);
                lcd.write(3);
                lcd.setCursor(positionx, 1);
              }
              else if(i == 51)
              {
                lcd.setCursor(positionx, 0);
                lcd.print(" ");
                lcd.setCursor(positionx, 1);
                lcd.write(8);
                lcd.setCursor(positionx, 0);
                lcd.write(4);
                lcd.setCursor(positionx, 1);
              }
              else if(i == 52)
              {
                lcd.setCursor(positionx, 0);
                lcd.print(" ");
                lcd.setCursor(positionx, 1);
                lcd.write(8);
                lcd.setCursor(positionx, 0);
                lcd.write(7);
                lcd.setCursor(positionx, 1);
              }
              else if(i == 53)
              {
                lcd.setCursor(positionx, 0);
                lcd.print(" ");
                lcd.setCursor(positionx, 1);
                lcd.write(8);
                lcd.setCursor(positionx, 0);
                lcd.write(7);
                lcd.setCursor(positionx, 1);
              }
              else if(i == 54)
              {
                lcd.setCursor(positionx, 0);
                lcd.print(" ");
                lcd.setCursor(positionx, 1);
                lcd.write(8);
                lcd.setCursor(positionx, 0);
                lcd.write(8);
                lcd.setCursor(positionx, 1);
              }
              positionx++;
            }
    }else
    {
    }
 }
