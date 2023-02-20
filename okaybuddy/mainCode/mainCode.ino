#include <N64Pad.h> // https://github.com/SukkoPera/N64PadForArduino
#include <Joystick.h>
#include <string.h>
#include <Keyboard.h> 



// Globals //

String serialNow = "";
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
//need to read these from eeprom
int button_values[] = {120, 121, 32, 99, KEY_UP_ARROW, KEY_DOWN_ARROW, KEY_LEFT_ARROW, KEY_RIGHT_ARROW, 122, 119, 154, 162};
int controllerbutton_values[] = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};


// Used for making sure that the DPAD functions correctly.
bool holdingUp = 0;
bool holdingDown = 0;
bool holdingLeft = 0;
bool holdingRight = 0;

//various mode related things
bool nesMode = false; 
bool snesMode = true; 
bool n64Mode = false; 
bool serialMode = false;

bool nesControllerConnected = false;
bool nesControllerConnectedLast = false; 
bool snesControllerConnected = false; 
bool snesControllerConnectedLast = false; 
bool n64ControllerConnected = false;

// modes for what the poutput will be 
bool keyboardMode = false; //output as keyboard
bool controllerMode = true; //output as controller

// Constants //

//n64stuff
const byte ANALOG_DEAD_ZONE = 20U;
const int8_t ANALOG_MIN_VALUE = -80;
const int8_t ANALOG_MAX_VALUE = 80;
const int8_t ANALOG_IDLE_VALUE = 0;

N64Pad pad;

Joystick_ usbStick (
	JOYSTICK_DEFAULT_REPORT_ID,
	JOYSTICK_TYPE_JOYSTICK,
	12,			// buttonCount
	0,			// hatSwitchCount (0-2)
	true,		// includeXAxis
	true,		// includeYAxis
	false,		// includeZAxis
	true,		// includeRxAxis
	true,		// includeRyAxis
	false,		// includeRzAxis
	false,		// includeRudder
	false,		// includeThrottle
	false,		// includeAccelerator
	false,		// includeBrake
	false		// includeSteering
);

#define deadify(var, thres) (abs (var) > thres ? (var) : 0)



// Controller I/O Pins
const int pulsePinNes = 0; 
const int latchPinNes = 1; 
const int dataPinNes = 2; 
//const int dataPinN64 = 3; //the n64 pad thing use pin 3 , this assingment is symbolic
const int pulsePinSnes = 4; 
const int latchPinSnes = 5; 
const int dataPinSnes = 6; 

// LED Indicator Pins
const int nesIndicateLed = 7;
const int snesIndicateLed = 8;
const int n64IndicateLed = 9;

//Switch ins
// SERIAL | NES | SNES | N64

const int serialSwitch = 10; 
const int nesSwitch = 11; 
const int snesSwitch = 12; 
const int n64Switch = 13; 








void flashLed (byte n) {
	for (byte i = 0; i < n; ++i) {
		digitalWrite (LED_BUILTIN, LOW);
		delay (40);
		digitalWrite (LED_BUILTIN, HIGH);
		delay (80);
	}
}

void checkSerialForEnd() {
    serialNow = Serial.readStringUntil('!');
    if (serialNow != "STOP") { //check for a serial request
        return; // else just continue
    }
    Serial.write("9999999999999999999999999999999999999999999999999999999999999999");
    Serial.write("9999999999999999999999999999999999999999999999999999999999999999");
    Serial.write("9999999999999999999999999999999999999999999999999999999999999999");
    Serial.write("9999999999999999999999999999999999999999999999999999999999999999");
    delay(120);
    Serial.flush();
    Serial.write("All done, ready to move on to the next.");
    loop(); //if there is a stop message, exit to the main loop
    
}

void checkButton(int button) {

    int button_index = button - 1;
      
    if (button_index > 11) return; // if we have passed all the buttons just return

    if (keyboardMode == true) {
        // if in nesMode and button index < 8 then according to the inversion of the state of dataPinNes press or release a button
        if (nesMode && button_index < 8) !digitalRead(dataPinNes) ? Keyboard.press(button_values[button_index]) : Keyboard.release(button_values[button_index]);

        // if in snesMode then according to the inversion of the state of dataPinSnes press or release a button
        if (snesMode) !digitalRead(dataPinSnes) ? Keyboard.press(button_values[button_index]) : Keyboard.release(button_values[button_index]);

        return;
    }
    
    if (controllerMode == true) {
        if (nesMode && button_index < 8) {
            if (button_index > 3) {
                if ((button_index == 4) && (digitalRead(dataPinNes) == HIGH)) { // Try to stop holding up
                    if (holdingDown == 0 && holdingUp == 1) { // check if we are holding up
                        usbStick.setYAxis(ANALOG_IDLE_VALUE); // release up
                        holdingUp = 0; // Let us know it's no longer held
                    } 
                }
                if ((button_index == 5) && (digitalRead(dataPinNes) == HIGH)) { // Try to stop holding down
                    if (holdingDown == 1 && holdingUp == 0) { // check if we are holding down
                        usbStick.setYAxis(ANALOG_IDLE_VALUE); // release down
                        holdingDown = 0; // Let us know it's no longer held
                    } 
                }
                if ((button_index == 4) && (digitalRead(dataPinNes) == LOW)) { //if 4th and read data from dataPin
                    usbStick.setYAxis(ANALOG_MIN_VALUE); // press up
                    holdingUp = 1; // let us know it is being held
                    holdingDown = 0; // cannot hold up and down at the same time
                }
                if ((button_index == 5) && (digitalRead(dataPinNes) == LOW)) { //if 5th and read data from dataPin
                    usbStick.setYAxis(ANALOG_MAX_VALUE); // press down
                    holdingDown = 1; // let us know it is being held
                    holdingUp = 0; // cannot hold up and down at the same time
                }


                if ((button_index == 6) && (digitalRead(dataPinNes) == HIGH)) { // Try to stop holding left
                    if (holdingRight == 0 && holdingLeft == 1) { // check if we are holding down
                        usbStick.setXAxis(ANALOG_IDLE_VALUE); // release left
                        holdingLeft = 0; // Let us know it's no longer held
                    } 
                }
                if ((button_index == 7) && (digitalRead(dataPinNes) == HIGH)) { // Try to stop holding right
                    if (holdingRight == 1 && holdingLeft == 0) { // check if we are holding down right
                        usbStick.setXAxis(ANALOG_IDLE_VALUE); // release
                        holdingRight = 0; // Let us know it's no longer held
                    } 
                }
                if ((button_index == 6) && (digitalRead(dataPinNes) == LOW)) { //if 6th and read data from dataPin
                    usbStick.setXAxis(ANALOG_MIN_VALUE); // press up
                    holdingLeft = 1; // let us know it is being held
                    holdingRight = 0; // cannot hold right and left at the same time
                }
                if ((button_index == 7) && (digitalRead(dataPinNes) == LOW)) { //if 7th and read data from dataPin
                    usbStick.setXAxis(ANALOG_MAX_VALUE); // press down
                    holdingRight = 1; // let us know it is being held
                    holdingLeft = 0; // cannot hold right and left at the same time
                }
                usbStick.sendState();
                return;
            }

            usbStick.setButton(controllerbutton_values[button_index], !digitalRead(dataPinNes)); // do the button at the index according to datapin read
            usbStick.sendState();
            return;
        }

        if (snesMode) {
            if (button_index > 3 && button_index < 8) {
                if ((button_index == 4) && (digitalRead(dataPinSnes) == HIGH)) { // Try to stop holding up
                    if (holdingDown == 0 && holdingUp == 1) { // check if we are holding up
                        usbStick.setYAxis(ANALOG_IDLE_VALUE); // release up
                        holdingUp = 0; // Let us know it's no longer held
                    } 
                }
                if ((button_index == 5) && (digitalRead(dataPinSnes) == HIGH)) { // Try to stop holding down
                    if (holdingDown == 1 && holdingUp == 0) { // check if we are holding down
                        usbStick.setYAxis(ANALOG_IDLE_VALUE); // release down
                        holdingDown = 0; // Let us know it's no longer held
                    } 
                }
                if ((button_index == 4) && (digitalRead(dataPinSnes) == LOW)) { //if 4th and read data from dataPin
                    usbStick.setYAxis(ANALOG_MIN_VALUE); // press up
                    holdingUp = 1; // let us know it is being held
                    holdingDown = 0; // cannot hold up and down at the same time
                }
                if ((button_index == 5) && (digitalRead(dataPinSnes) == LOW)) { //if 5th and read data from dataPin
                    usbStick.setYAxis(ANALOG_MAX_VALUE); // press down
                    holdingDown = 1; // let us know it is being held
                    holdingUp = 0; // cannot hold up and down at the same time
                }


                if ((button_index == 6) && (digitalRead(dataPinSnes) == HIGH)) { // Try to stop holding left
                    if (holdingRight == 0 && holdingLeft == 1) { // check if we are holding down
                        usbStick.setXAxis(ANALOG_IDLE_VALUE); // release left
                        holdingLeft = 0; // Let us know it's no longer held
                    } 
                }
                if ((button_index == 7) && (digitalRead(dataPinSnes) == HIGH)) { // Try to stop holding right
                    if (holdingRight == 1 && holdingLeft == 0) { // check if we are holding down right
                        usbStick.setXAxis(ANALOG_IDLE_VALUE); // release
                        holdingRight = 0; // Let us know it's no longer held
                    } 
                }
                if ((button_index == 6) && (digitalRead(dataPinSnes) == LOW)) { //if 6th and read data from dataPin
                    usbStick.setXAxis(ANALOG_MIN_VALUE); // press up
                    holdingLeft = 1; // let us know it is being held
                    holdingRight = 0; // cannot hold right and left at the same time
                }
                if ((button_index == 7) && (digitalRead(dataPinSnes) == LOW)) { //if 7th and read data from dataPin
                    usbStick.setXAxis(ANALOG_MAX_VALUE); // press down
                    holdingRight = 1; // let us know it is being held
                    holdingLeft = 0; // cannot hold right and left at the same time
                }
                usbStick.sendState();
                return;
            }

            usbStick.setButton(controllerbutton_values[button_index], !digitalRead(dataPinSnes)); // do the button at the index according to datapin read
            usbStick.sendState();
            return;
        }
        return;
    }
    return; // safety return. THIS WILL NOT HAPPEN.
}

void clearAllButtons() {
    usbStick.setYAxis(ANALOG_IDLE_VALUE);
    usbStick.setXAxis(ANALOG_IDLE_VALUE);
    for(int u = 0; u < 12; u++){
        usbStick.setButton(controllerbutton_values[u-1], 0);
    }
    usbStick.sendState();
    Keyboard.releaseAll();
}

void serialActions() {

    serialNow = Serial.readStringUntil('!');
    if (serialNow != "SREQ") { //check for a serial request
        return; // there was no request, return
    }
    Serial.write("SACK"); //send acknowledge with the delimiter
    while (true){ //wait for a command
        serialNow = Serial.readStringUntil('!');

        if (serialNow == "REMAP") { // the command is remap

            Serial.write("REMAPACK"); // ackowledge the command

            while (true) {  // wait for a system identifier 

                serialNow = Serial.readStringUntil('!');

                if (serialNow == "NES") { 
                    Serial.write("NESACK");

                    while(true){
                        serialNow = Serial.readStringUntil('!');
                        Serial.write("Got: ");
                        Serial.write(serialNow.c_str());
                        checkSerialForEnd();
                    }
                }
                if (serialNow == "SNES") {
                    Serial.write("SNESACK");

                    while(true){
                        serialNow = Serial.readStringUntil('!');
                        Serial.write("Got: ");
                        Serial.write(serialNow.c_str());
                        if (serialNow == "DONE"){
                            Serial.write("Ready for something else.");
                            return;
                        }
                        checkSerialForEnd();
                    }
                }
                if (serialNow == "N64") {
                    Serial.write("N64ACK");

                    while(true){
                        serialNow = Serial.readStringUntil('!');
                        Serial.write("Got: ");
                        Serial.write(serialNow.c_str());
                        if (serialNow == "DONE"){
                            Serial.write("Ready for something else.");
                            return;
                        }
                        checkSerialForEnd();
                    }
                }
                checkSerialForEnd();
            }   
        }
        checkSerialForEnd();
    }
}

void nes() {
    nesControllerConnectedLast = nesControllerConnected; // Store the last known state of of the controllers connection

    digitalWrite(latchPinNes, HIGH);
    if (nesControllerConnected) checkButton(1); // check for A here

    delayMicroseconds(12);

    digitalWrite(latchPinNes, LOW);
    delayMicroseconds(6);
    

    for (int j = 2; j < 9; j++)
    {
        // pulse the pulse pin

        //6 high
        digitalWrite(pulsePinNes, HIGH);
        // check for the rest of the buttons
        if (nesControllerConnected) checkButton(j); 
        delayMicroseconds(6);

        //6 low
        digitalWrite(pulsePinNes, LOW);
        delayMicroseconds(6);
    }

    // Pulse genreation and check for controller
    for (int e = 10; e < 17; e++) {
        // pulse the pulse pin
        //6 high
        digitalWrite(pulsePinSnes, HIGH);
        digitalRead(dataPinNes) ? nesControllerConnected = true : nesControllerConnected = false; // If data pin is high here, a controller is connected.
        delayMicroseconds(6);
        //6 low
        digitalWrite(pulsePinNes, LOW);
        delayMicroseconds(6);
    }

    if(!nesControllerConnected && (nesControllerConnected != nesControllerConnectedLast)) { // if a NES controller is not connected, and the connection has changed 
        clearAllButtons(); // need to only do this when the controller actually becomes disconnected
    }
}   

void snes() {
    snesControllerConnectedLast = snesControllerConnected; // Store the last known state of of the controllers connection
    // SNES latch signal generation
    digitalWrite(latchPinSnes, HIGH);
    if (snesControllerConnected) checkButton(1); // first button, only read a button input if a controller is connected

    delayMicroseconds(12);

    digitalWrite(latchPinSnes, LOW);
    delayMicroseconds(6);
    
    // Pulse genreation and read for rest of buttons
    for (int j = 2; j < 13; j++)
    {
        // pulse the pulse pin

        //6 high
        digitalWrite(pulsePinSnes, HIGH);   
        if (snesControllerConnected) checkButton(j); // check for the rest of the buttons, only read a button input if a controller is connected
        delayMicroseconds(6);

        //6 low
        digitalWrite(pulsePinSnes, LOW);
        delayMicroseconds(6);
    }

    // Pulse genreation and check for controller
    for (int t = 13; t < 17; t++) {
        // pulse the pulse pin
        //6 high
        digitalWrite(pulsePinSnes, HIGH);
        digitalRead(dataPinSnes) ? snesControllerConnected = true : snesControllerConnected = false; // If data pin is high here, a controller is connected.
        delayMicroseconds(6);
        //6 low
        digitalWrite(pulsePinSnes, LOW);
        delayMicroseconds(6);
    }

    if(!snesControllerConnected && (snesControllerConnected != snesControllerConnectedLast)) { // if a SNES controller is not connected, and the connection has changed 
        clearAllButtons(); // need to only do this when the controller actually becomes disconnected
    }
}  

void n64() {
    //if (!n64ControllerConnected) return;

	if (!pad.read()) {
		// Controller lost :(;
		n64ControllerConnected = false;
	} 
    else {
		// Controller was read fine
    
		// Map buttons!
        //if ((pad.buttons & N64Pad::BTN_B) != 0) {
        //   usbStick.setButton (0, 1);
        //}
        //if ((pad.buttons & N64Pad::BTN_B) == 0) {
        //    usbStick.setButton (0, 0);
        //}
		usbStick.setButton(0, (pad.buttons & N64Pad::BTN_B) != 0);
		usbStick.setButton(1, (pad.buttons & N64Pad::BTN_A) != 0);
		usbStick.setButton(2, (pad.buttons & N64Pad::BTN_C_LEFT) != 0);
		usbStick.setButton(3, (pad.buttons & N64Pad::BTN_C_DOWN) != 0);
		usbStick.setButton(4, (pad.buttons & N64Pad::BTN_C_UP) != 0);
		usbStick.setButton(5, (pad.buttons & N64Pad::BTN_C_RIGHT) != 0);
		usbStick.setButton(6, (pad.buttons & N64Pad::BTN_L) != 0);
		usbStick.setButton(7, (pad.buttons & N64Pad::BTN_R) != 0);
		usbStick.setButton(8, (pad.buttons & N64Pad::BTN_Z) != 0);
		usbStick.setButton(9, (pad.buttons & N64Pad::BTN_START) != 0);
		// D-Pad makes up the X/Y axes
		if ((pad.buttons & N64Pad::BTN_UP) != 0) {
			usbStick.setYAxis(ANALOG_MIN_VALUE);
		} 
        else if ((pad.buttons & N64Pad::BTN_DOWN) != 0) {
			usbStick.setYAxis(ANALOG_MAX_VALUE);
		} 
        else {
			usbStick.setYAxis(ANALOG_IDLE_VALUE);
		}
		if ((pad.buttons & N64Pad::BTN_LEFT) != 0) {
			usbStick.setXAxis(ANALOG_MIN_VALUE);
		} 
        else if ((pad.buttons & N64Pad::BTN_RIGHT) != 0) {
			usbStick.setXAxis(ANALOG_MAX_VALUE);
		} 
        else {
			usbStick.setXAxis(ANALOG_IDLE_VALUE);
		}
		// The analog stick gets mapped to the X/Y rotation axes
		usbStick.setRxAxis (pad.x);
		usbStick.setRyAxis (pad.y);
		
		// All done, send data for real!
		usbStick.sendState ();
		
	} 
}

void checkSwitches() {
    if ( (!digitalRead(nesSwitch) + !digitalRead(snesSwitch) + !digitalRead(n64Switch)) > 1  ) { // WHat do you mean, more then one is activated at a time?!
    Serial.write("Error: Too many selected.");
        serialMode = false;  
        nesMode = false;
        snesMode = false;
        n64Mode = false;
    }
    else if (!digitalRead(nesSwitch)) {
        serialMode = false;
        nesMode = true;     //
        snesMode = false;
        n64Mode = false;
    }
    else if (!digitalRead(snesSwitch)) {
        serialMode = false;
        nesMode = false;
        snesMode = true;    //
        n64Mode = false;
    }
    else if (!digitalRead(n64Switch)) {
        serialMode = false;
        nesMode = false;
        snesMode = false;
        n64Mode = true;     //
    }
    else if (!digitalRead(serialSwitch)) {
        serialMode = true; // 
        nesMode = false;
        snesMode = false;
        n64Mode = false;
    }
    else {//fallback to serial
        serialMode = true; 
        nesMode = false;
        snesMode = false;
        n64Mode = false;
    }
}

void setup() {
    Serial.begin(9600);

    pinMode(pulsePinNes, OUTPUT);
    pinMode(latchPinNes, OUTPUT);
    pinMode(dataPinNes, INPUT);

    pinMode(pulsePinSnes, OUTPUT);
    pinMode(latchPinSnes, OUTPUT);
    pinMode(dataPinSnes, INPUT);

    pinMode(serialSwitch, INPUT_PULLUP);
    pinMode(nesSwitch, INPUT_PULLUP); 
    pinMode(snesSwitch, INPUT_PULLUP);
    pinMode(n64Switch, INPUT_PULLUP);

    pinMode(nesIndicateLed, OUTPUT);
    pinMode(snesIndicateLed, OUTPUT);
    pinMode(n64IndicateLed, OUTPUT);

    usbStick.begin (false);		// We'll call sendState() manually to minimize lag. I didn't say this, who else is here?
	usbStick.setXAxisRange (ANALOG_MIN_VALUE, ANALOG_MAX_VALUE);
	usbStick.setYAxisRange (ANALOG_MIN_VALUE, ANALOG_MAX_VALUE);
	usbStick.setRxAxisRange (ANALOG_MIN_VALUE, ANALOG_MAX_VALUE);
	usbStick.setRyAxisRange (ANALOG_MAX_VALUE, ANALOG_MIN_VALUE);
}

void loop() {  

    snesControllerConnected ? digitalWrite (LED_BUILTIN, HIGH): digitalWrite (LED_BUILTIN, LOW);

    //checkSwitches();
    if (serialMode == true) serialActions();
    
    if (nesMode == true) nes();
    
    if (snesMode == true) snes();
    
    if (n64Mode == true) n64();
}