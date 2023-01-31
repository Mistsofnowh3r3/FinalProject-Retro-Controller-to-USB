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

// main program
void loop() {
    startMillis = millis(); //start the clock
    currentMillis = millis(); // get the current time

    for (int i = 18; currentMillis - startMillis < i; i += 0) {
        currentMillis = millis(); // get the current time

        //12 high 6 low
        if (i < 12) {
            digitalWrite(latchPin, HIGH);
        } else {
            digitalWrite(latchPin, LOW);
        }

        //check for a button (active low) at any point here    
        if ((digitalRead(dataPin) == false)) { // if A button is low press the A keyboard key
            //Keyboard.press(97);
            Serial.print("A button was here");
        } else { // otherwise release the key ( should this instead be at the start of the loop?)
            Keyboard.release(97);
        }

    }

    // replace with Millis method and also add checks for all other buttons 
    for (int j = 1; j < 9; j++) {
        pulsePulse();
    }
}