#include <Keyboard.h> // using keyboard for now. will do USB controller eventually 

//declare globals
int lastButtonState = 1;    // previous state of the button
int buttonState = 1;
// string controller = null; // current controller

//declare constants
const int button_values[] = {97, 98, 32, KEY_RETURN, KEY_UP_ARROW, KEY_DOWN_ARROW, KEY_LEFT_ARROW, KEY_RIGHT_ARROW, 122, 120, 154, 162};
const int pulsePin = 1; // the number of the pushbutton pin
const int latchPin = 2; // the number of the LED pin
const int dataPin = 3; // the number of the pushbutton pin
// const int switch =  ; // state of the selection switch

//various mode related things
const bool nesMode = false; // cureently ditactes if snes or nes
const bool snesMode = true; // cureently ditactes if snes or nes
const bool snesMouseMode = true; // cureently ditactes if snes or nes
const bool n64Mode = false; // cureently ditactes if snes or nes

// modes for what the poutput will be 
const bool edMode = false; // control bindings set to ED compatible
const bool keyboardMode = true; //output as keyboard
const bool xinputMode = false; //output as xinput controller




void checkButton(int button, bool mode) {

    // if mode = 0 check for press if 1 check for release
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
    // partial chatgpt helped code that makes my original more managable
    


    int button_index = button - 1;
      
    if (button_index > 12) { // if we have past all the buttons just return and do nothing
        return;
    }

    if (!snesMode || button_index < 8) {  // if not in SNES mode or below 8
        
        if ((mode == false) && (digitalRead(dataPin) == false)) {
            Keyboard.press(button_values[button_index]);
            Serial.print((char) button_values[button_index]);
            return;
        }
        if ((mode == true) && (digitalRead(dataPin) == true)) {
            Keyboard.release(button_values[button_index]);
            return;
        }

    }	
    // get here if in snes mode if in SNES mode we do the keys past the 8th one
    if ((mode == false) && (digitalRead(dataPin) == false)) {
        Keyboard.press(button_values[button_index]);
        Serial.print((char) button_values[button_index]);
        return;
    }
    if ((mode == true) && (digitalRead(dataPin) == true)) {
        Keyboard.release(button_values[button_index]);
        return;
    }

}

void nes() {
    //noInterrupts();
    //interrupts();



    digitalWrite(latchPin, HIGH);
    checkButton(1, 0); // check for A here
    checkButton(1, 1);

    delayMicroseconds(12);

    
    digitalWrite(latchPin, LOW);
    delayMicroseconds(6);
    

    for (int j = 2; j < 10; j++)
    {
        // pulse the pulse pin

        //6 high
        digitalWrite(pulsePin, HIGH);
        // check for the rest of the buttons
        checkButton(j, 0); 
        checkButton(j, 1);
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
    checkButton(1, false); // check for A here
    checkButton(1, true);

    delayMicroseconds(12);

    
    digitalWrite(latchPin, LOW);
    delayMicroseconds(6);
    

    for (int j = 2; j < 18; j++)
    {
        // pulse the pulse pin

        //6 high
        digitalWrite(pulsePin, HIGH);
        // check for the rest of the buttons
        checkButton(j, false); 
        checkButton(j, true);
        delayMicroseconds(6);

        //6 low
        digitalWrite(pulsePin, LOW);
        delayMicroseconds(6);


    }

}  

void setup() {
    
    // set the digital pin STATES
    pinMode(pulsePin, OUTPUT);
    pinMode(latchPin, OUTPUT);
    pinMode(dataPin, INPUT);

}

void loop() {

    for ( int t = 1; t < 61; t++){ // try to only do the thing 60 times a second
        snes();
        delayMicroseconds(16550); 
    }

}