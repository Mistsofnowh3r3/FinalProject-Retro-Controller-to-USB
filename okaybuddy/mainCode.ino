#include <Keyboard.h> // using keyboard for now. will do USB controller eventually 

//need to move to interrupts

//declare globals
unsigned long startMillis;
unsigned long currentMillis;

int buttonPushCounter = 0;  // counter for the number of button presses
int buttonState = 0;        // current state of the button
int lastButtonState = 0;    // previous state of the button

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


bool detectEdge() { // modification of https://www.arduino.cc/en/Tutorial/BuiltInExamples/StateChangeDetection
  // read the pushbutton input pin:
  buttonState = digitalRead(dataPin);

  // compare the buttonState to its previous state
  if (buttonState != lastButtonState) {
    // if the state has changed, increment the counter
    if (buttonState == HIGH) 
    {
      // if the current state is HIGH then the button went from off to on:
        buttonPushCounter++;
        lastButtonState = buttonState;
        return true;
    } 
    else 
    {
      // if the current state is LOW then the button went from on to off:
        lastButtonState = buttonState;
        return false;
    }   
  }
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

}

void checkButtonRelease(int button){
    
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
    for (int i = 1; currentMillis - startMillis < 18; i++) { //helps to actually increment i 
        currentMillis = millis(); // get the current time
        
        //12 high 6 low
        if (i < 13) 
        {
            digitalWrite(latchPin, HIGH);
            checkButtonRelease(1);
            checkButton(1); // check for A here
        }
        else 
        {
            digitalWrite(latchPin, LOW);
            //checkButton(1); // check for A here
        }
        
        

  
     }
    // replace with Millis method and also add checks for all other buttons 
    for (int j = 2; j < 9; j++)
    {
        startMillis = millis(); //start or restart the clock
        currentMillis = millis(); // get the current time

        for (int k = 2; currentMillis - startMillis < 12; k++) { //helps to actually increment i 
            currentMillis = millis(); // get the current time
            
            //6 high 6 low
            if (k < 8) 
            {
               digitalWrite(pulsePin, HIGH);
               checkButtonRelease(j); // check for a button here
               checkButton(j); // check for a button here
            }
            else 
            {
               digitalWrite(pulsePin, LOW);
               //checkButton(j); // check for a button here
            }
            
        }
    }
}