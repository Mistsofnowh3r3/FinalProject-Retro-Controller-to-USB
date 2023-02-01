#include <Keyboard.h> // using keyboard for now. will do USB controller eventually 
//need to move to interrupts

//declare globals
unsigned long startMicros;
unsigned long currentMicros;
unsigned long TIMESTART;
unsigned long TIMEEND;

int lastButtonState = 1;    // previous state of the button
int buttonState = 1;
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

int detectEdge() { // modification of https://www.arduino.cc/en/Tutorial/BuiltInExamples/StateChangeDetection
    buttonState = digitalRead(dataPin);
  if (buttonState != lastButtonState) {
    lastButtonState = buttonState;
    Serial.print(buttonState == 0) ? 0 : 1;
    return (buttonState == 0) ? 0 : 1;
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
    
    //snes
    //9 = A
    //10 = X
    //11 = L
    //12 = R

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
        
        //SNES
        case 9  : //A
            Keyboard.press(122);
            Serial.print("z");
            break; 
        
        case 10  : //X 
            Keyboard.press(120);
            Serial.print("x");
            break; 
        
        case 11  : //L 
            Keyboard.press(154);
            Serial.print("l");
            break; 
        
        case 12  : //R 
            Keyboard.press(162);
            Serial.print("r");
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

        //SNES
        case 9  : //A
            Keyboard.release(122);
            Serial.print("z");
            break; 
        
        case 10  : //X 
            Keyboard.release(120);
            Serial.print("x");
            break; 
        
        case 11  : //L 
            Keyboard.release(154);
            Serial.print("l");
            break; 
        
        case 12  : //R 
            Keyboard.release(162);
            Serial.print("r");
            break; 
        default  :  break;
        }
    }
}

// main program
void nes() {
    //noInterrupts();
    //interrupts();



    digitalWrite(latchPin, HIGH);
    checkButton(1); // check for A here
    checkButtonRelease(1);

    delayMicroseconds(12);

    
    digitalWrite(latchPin, LOW);
    delayMicroseconds(6);
    

    for (int j = 2; j < 10; j++)
    {
        // pulse the pulse pin

        //6 high
        digitalWrite(pulsePin, HIGH);
        // check for the rest of the buttons
        checkButton(j); 
        checkButtonRelease(j);
        delayMicroseconds(6);

        //6 low
        digitalWrite(pulsePin, LOW);
        delayMicroseconds(6);


    }

}   

void snes() {
    //noInterrupts();
    //interrupts();



    digitalWrite(latchPin, HIGH);
    checkButton(1); // check for A here
    checkButtonRelease(1);

    delayMicroseconds(12);

    
    digitalWrite(latchPin, LOW);
    delayMicroseconds(6);
    

    for (int j = 2; j < 18; j++)
    {
        // pulse the pulse pin

        //6 high
        digitalWrite(pulsePin, HIGH);
        // check for the rest of the buttons
        checkButton(j); 
        checkButtonRelease(j);
        delayMicroseconds(6);

        //6 low
        digitalWrite(pulsePin, LOW);
        delayMicroseconds(6);


    }

}  

void loop() {

    TIMESTART = micros();
    for ( int t = 1; t < 61; t++){ // try to only do the thing 60 times a second
        snes();
        delayMicroseconds(16550); 
    }

}