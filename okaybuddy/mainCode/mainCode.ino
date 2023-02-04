#include <Keyboard.h> // using keyboard for now. will do USB controller eventually 
#include "Mouse.h"
#include <XInput.h>
//declare globals
int lastButtonState = 1;    // previous state of the button
int buttonState = 1;
int mouseX = 0;
int mouseLastX = 0;
int mouseXDirection = 0;
int mouseY = 0;
int mouseLastY = 0;
int mouseYDirection = 0;
int doesa = 0; // se the sensitivity at least once

// string controller = null; // current controller

//declare constants
const int button_values[] = {97, 98, 32, KEY_RETURN, KEY_UP_ARROW, KEY_DOWN_ARROW, KEY_LEFT_ARROW, KEY_RIGHT_ARROW, 122, 120, 154, 162};
const int pulsePin = 1; // the number of the pushbutton pin
const int latchPin = 2; // the number of the LED pin
const int dataPin = 3; // the number of the pushbutton pin

const int sanityOut = 5; // the number of the pushbutton pin
const int sanityIn = 6; // the number of the pushbutton pin

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


bool snesMouseCheck(bool mode) {

    //mode
    //0 is checking for a hit (LOW)
    //1 is checking for a release (High)


    if ((mode == false) && (digitalRead(dataPin) == false)) {
        //line went false, we got a hit
        return true;
    }
    if ((mode == true) && (digitalRead(dataPin) == true)) {
        //line went TRUE, it was release, or was never pressed
        return true;
    }
    return false;
}

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
            Serial.print(button_values[button_index]);
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
        Serial.print(button_values[button_index]);
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
    if (snesMouseMode == true) {



    }

}  

void snesMouse() {



    //DATA LATCH 1
    digitalWrite(latchPin, HIGH);

    
    //if (doesa == 0 ) {
    //    digitalWrite(pulsePin, HIGH);
    //    doesa = 1;
    //    digitalWrite(pulsePin, LOW);
    //}
    
    delayMicroseconds(12);
    digitalWrite(latchPin, LOW);
    delayMicroseconds(6);
    
    // DO 7 PULSES
    #pragma region // 2 - 8
    for (int f = 0; f < 7; f++) {
        //6 high
        digitalWrite(pulsePin, HIGH);
        delayMicroseconds(6);
        //6 low
        digitalWrite(pulsePin, LOW);
        delayMicroseconds(6);
    }
    #pragma endregion

    #pragma region  //rightmouse 9
    //6 high
    digitalWrite(pulsePin, HIGH);
    delayMicroseconds(6);

    if (snesMouseCheck(0)) {
        //pressmouse
    }
    if (snesMouseCheck(1)) {
        //releasemouse
    }
    //6 low
    digitalWrite(pulsePin, LOW);
    delayMicroseconds(6);
    #pragma endregion

    #pragma region  //leftmouse 10
    //6 high
    digitalWrite(pulsePin, HIGH);
    delayMicroseconds(6);

    if (snesMouseCheck(0)) {
        //pressmouse
    }
    if (snesMouseCheck(1)) {
        //releasemouse
    }
    //6 low
    digitalWrite(pulsePin, LOW);
    delayMicroseconds(6);
    #pragma endregion

    #pragma region  //speedbit? 11
    //6 high
    digitalWrite(pulsePin, HIGH);
    delayMicroseconds(6);
    //6 low
    digitalWrite(pulsePin, LOW);
    delayMicroseconds(6);
    #pragma endregion

    #pragma region  //speedbit again 12
    //6 high
    digitalWrite(pulsePin, HIGH);
    delayMicroseconds(6);
    //6 low
    digitalWrite(pulsePin, LOW);
    delayMicroseconds(6);
    #pragma endregion

    #pragma region // 13 - 16
    for (int h = 0; h < 4; h++) {
        //6 high
        digitalWrite(pulsePin, HIGH);
        delayMicroseconds(6);
        //6 low
        digitalWrite(pulsePin, LOW);
        delayMicroseconds(6);
    }
    #pragma endregion

    #pragma region  //leftmouse 17 Y direction (0=up, 1=down)
    //1 high
    digitalWrite(pulsePin, HIGH);
    delayMicroseconds(.5);

    if (!digitalRead(dataPin)) {
        mouseYDirection = 1;
        //Down
    }
    else {
        mouseYDirection = 0;
    }
    //8 low
    digitalWrite(pulsePin, LOW);
    delayMicroseconds(8);
    #pragma endregion

    #pragma region  //leftmouse 18 - 24 Y motion
    //1 high
    for ( int q = 0; q < 7; q++) {
        digitalWrite(pulsePin, HIGH);
        mouseY = mouseY << 1;
        mouseY = mouseY | (!digitalRead(dataPin));
        delayMicroseconds(.5);
        //8 low
        digitalWrite(pulsePin, LOW);
        delayMicroseconds(8);
    }

    #pragma endregion




    #pragma region  //leftmouse 25 X direction (0=left, 1=right)
    //1 high
    digitalWrite(pulsePin, HIGH);
    delayMicroseconds(.5);

    if (!digitalRead(dataPin)) {
        mouseXDirection = 1;
        //left
    }
    else {
        mouseXDirection = 0;
    }
    //8 low
    digitalWrite(pulsePin, LOW);
    delayMicroseconds(8);
    #pragma endregion

    #pragma region  //leftmouse 25 - 32 X motion
    //1 high
    for ( int q = 0; q < 7; q++) {
        digitalWrite(pulsePin, HIGH);
        mouseX = mouseX << 1;
        mouseX = mouseX | (!digitalRead(dataPin));
        delayMicroseconds(.5);
        //8 low
        digitalWrite(pulsePin, LOW);
        delayMicroseconds(8);
    }

    #pragma endregion

    if (mouseYDirection == 1) {
        Mouse.move(0, ( -1 * (mouseLastY - mouseY)));
    }
    else {
        Mouse.move(0, mouseLastY - mouseY);
    }

    if (mouseXDirection == 1) {
        Mouse.move(( -1 * (mouseLastX - mouseX)), 0);
    }
    else {
        Mouse.move(mouseLastX - mouseX, 0);
    }

    

    mouseLastX = mouseX;
    mouseLastY = mouseY;
    mouseX = 0;
    mouseY = 0;
}  


void setup() {
    Serial.begin(9600);
    // set the digital pin STATES
    pinMode(pulsePin, OUTPUT);
    pinMode(latchPin, OUTPUT);
    pinMode(dataPin, INPUT);
    pinMode(sanityIn, INPUT);

}

void loop() {

    
    for ( int t = 1; t < 61; t++){ // try to only do the thing 60 times a second
        if (digitalRead(sanityIn) == true){ //when 6 is grounded, we do not go
            snes();
            delayMicroseconds(16550); 
        }
    }

}