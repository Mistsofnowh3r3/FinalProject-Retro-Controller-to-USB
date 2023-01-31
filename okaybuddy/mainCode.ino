#include <Keyboard.h> // using keyboard for now. will do USB controller eventually 

//declare globals
unsigned long startMillis;
unsigned long currentMillis;
// string controller = null; // current controller

//declare constants
const int pulsePin = 1; // the number of the pushbutton pin
const int latchPin = 2; // the number of the LED pin
const int dataPin = 3; // the number of the pushbutton pin
// const int switch =  ; // state of the selection switch

void setup() {

    // set the digital pin STATES
    pinMode(pulsePin, OUTPUT);
    pinMode(latchPin, OUTPUT);
    pinMode(dataPin, INPUT);
}

// pulse Pulse 
void pulsePulse() {
    digitalWrite(pulsePin, HIGH);
    delay(6);
    digitalWrite(pulsePin, LOW);
    delay(6);
}

void checkButton(int button) {
    //1 = A 
    //2 = B
    //3 = SELECT
    //4 = START
    //5 = UP
    //6 = DOWN
    //7 = LEFT
    //8 = RIGHT

    if (digitalRead(dataPin) == false) // FIRST CHECK IF A KEY IS PRESSED (active low)
    { 
        switch(button) {

        case 1  : //A 
            Keyboard.press(97);
            Serial.print("a");
            break; 
        
        case 2  : //B 
            Keyboard.press(98);
            Serial.print("b");
            break; 
        
        case 3  : //SELECT 
            Keyboard.press(32);
            Serial.print("SPACE");
            break; 
        
        case 4  : //START 
            Keyboard.press(KEY_RETURN);
            Serial.print("ENTER");
            break; 
        
        case 5  : //UP 
            Keyboard.press(KEY_UP_ARROW);
            Serial.print("UP");
            break; 
        
        case 6  : //DOWN 
            Keyboard.press(KEY_DOWN_ARROW);
            Serial.print("DOWN");
            break; 
        
        case 7  : //LEFT 
            Keyboard.press(KEY_LEFT_ARROW);
            Serial.print("LEFT");
            break; 
        
        case 8  : //RIGHT 
            Keyboard.press(KEY_RIGHT_ARROW);
            Serial.print("RIGHT");
            break; 

        default  :  break;
        }
    }

    if (digitalRead(dataPin) == true) // now check if it was released
    { 
        switch(button) {

        case 1  : //A 
            Keyboard.release(97);
            break; 
        
        case 2  : //B 
            Keyboard.release(98);
            break; 
        
        case 3  : //SELECT 
            Keyboard.release(32);
            break; 
        
        case 4  : //START 
            Keyboard.release(KEY_RETURN);
            break; 
        
        case 5  : //UP 
            Keyboard.release(KEY_UP_ARROW);
            break; 
        
        case 6  : //DOWN 
            Keyboard.release(KEY_DOWN_ARROW);
            break; 
        
        case 7  : //LEFT 
            Keyboard.release(KEY_LEFT_ARROW);
            break; 
        
        case 8  : //RIGHT 
            Keyboard.release(KEY_RIGHT_ARROW);
            break; 

        default  :  break;
        }
    }

}



// main program
void loop() {
    startMillis = millis(); //start or restart the clock
    currentMillis = millis(); // get the current time

    for (int i = 1; currentMillis - startMillis < 19; i++) { //helps to actually increment i 
        currentMillis = millis(); // get the current time

        //12 high 6 low
        if (i < 13) 
        {
            digitalWrite(latchPin, HIGH);
        }
        else 
        {
            digitalWrite(latchPin, LOW);
        }

        checkButton(1); // check for a here

    }

    // replace with Millis method and also add checks for all other buttons 
    for (int j = 2; j < 11; j++)
    {
        startMillis = millis(); //start or restart the clock
        currentMillis = millis(); // get the current time

        for (int k = 1; currentMillis - startMillis < 13; k++) { //helps to actually increment i 
            currentMillis = millis(); // get the current time

            //6 high 6 low
            if (k < 7) 
            {
               digitalWrite(pulsePin, HIGH);
            }
            else 
            {
               digitalWrite(pulsePin, LOW);
            }

            checkButton(j); // check for a button here

        }
    }
}