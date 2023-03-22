#include <N64Pad.h> // https://github.com/SukkoPera/N64PadForArduino
#include <Joystick.h>
#include <string.h>
#include <Keyboard.h> 
#include <EEPROM.h>
#include <stdio.h>


// Globals //

String serialNow = "";
String parts[4]; // create an array to hold the three substrings

//EEPROM MEMORY MAP
//0 NES A
//1 NES B
//2 NES SELECT
//3 NES START
//4 NES UP
//5 NES DOWN
//6 NES LEFT
//7 NES RIGHT
//8 SNES B
//9 SNES Y
//10 SNES UP
//11 SNES DOWN
//12 SNES LEFT
//13 SNES RIGHT
//14 SNES A 
//15 SNES X
//16 SNES L
//17 SNES R
//18 N64 //A     
//19 N64 //B
//20 N64 //Z
//21 N64 //START
//22 N64 //D UP
//23 N64 //D DOWN
//24 N64 //D LEFT
//25 N64 //D RIGHT
//26 N64 //C DOWN
//27 N64 //C LEFT
//28 N64 //L
//29 N64 //R
//30 N64 //C UP
//31 N64 //C RIGHT
//32 N64 //A UP
//33 N64 //A DOWN
//34 N64 //A LEFT
//35 N64 //A RIGHT



int init_NES_btns[] = {
    'z',            //A
    'x',            //B
    KEY_RETURN,     //SELECT
    32,             //START
    KEY_UP_ARROW,   //UP   
    KEY_DOWN_ARROW, //DOWN
    KEY_LEFT_ARROW, //LEFT
    KEY_RIGHT_ARROW,//RIGHT
};
int held_NES_btns[8];

int init_SNES_btns[] = {
    'z',            //B
    'x',            //Y
    KEY_RETURN,     //SELECT
    32,             //START
    KEY_UP_ARROW,   //UP   
    KEY_DOWN_ARROW, //DOWN
    KEY_LEFT_ARROW, //LEFT
    KEY_RIGHT_ARROW,//RIGHT
    'a',            //A
    's',            //X
    'q',            //L
    'w',            //R
};
int held_SNES_btns[12];

int init_N64_btns[] = {
    'z',            //A     
    'x',            //B
    KEY_RETURN,     //Z
    32,             //START
    KEY_UP_ARROW,   //D UP
    KEY_DOWN_ARROW, //D DOWN
    KEY_LEFT_ARROW, //D LEFT
    KEY_RIGHT_ARROW,//D RIGHT
    'k',            //C DOWN
    'j',            //C LEFT
    'q',            //L
    'e',            //R
    'i',            //C UP
    'l',            //C RIGHT
    'w',            //A UP
    's',            //A DOWN
    'a',            //A LEFT
    'd',            //A RIGHT
};
int held_N64_btns[18];

int controllerbutton_values[] = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};
int n64KeyJoystickDeadZone = 20;

// Used for making sure that the DPAD functions correctly.
int holdCheckUpDown = 5;
int holdCheckLeftRight = 5;
int holdCheckUpDownN64Joy = 5;
int holdCheckLeftRightN64Joy = 5;

//various mode related things
// 0 serial, 1 NES, 2 SNES, 3 N64
int modeSelect = 0;
int modeSelectLast = 4;



bool nesControllerConnected = false;
bool nesControllerConnectedLast = false; 
bool snesControllerConnected = false; 
bool snesControllerConnectedLast = false; 
bool n64ControllerConnected = false;

// Controller vs kyeboard mode select keyboard = 1 controller = 0 
bool outputMode = 1;

// Constants //

//n64stuff
const byte ANALOG_DEAD_ZONE = 20U;
const int8_t ANALOG_MIN_VALUE = -80;
const int8_t ANALOG_MAX_VALUE = 80;
const int8_t ANALOG_IDLE_VALUE = 0;

N64Pad pad;

//sets up the joystick
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
const int outModeSwitch = PIN_A5; 


void serialActions() {
    clearAllButtons(); 
    memset(parts, 0, sizeof(parts));  // clear the parts array
    serialNow = Serial.readStringUntil('!');  // read the command

    // ChatGPT code, document and undestand it better.<
    int partIndex = 0; // initialize the array index
    for (int i = 0; i < serialNow.length(); i++) {
        
        char c = serialNow.charAt(i);
        if (c == ',') { // if the character is a comma, move to the next substring
            partIndex++;
        } 
        else if (c != '!') { // if the character is not a comma or exclamation mark, add it to the current substring
            parts[partIndex] += c;
        }
    }
    // >END CHAT GPT CODE
    if (parts[0] == "PO") { // POKE
        int adr = parts[1].toInt();
        int val = parts[2].toInt();

        if(parts[3] == "NES") {
            adr += 0;
        }
        else if(parts[3] == "SNES") {
            adr += 8;
        }
        else if(parts[3] == "N64") {
            adr += 18;
        }
        //Serial.write("Wrote: " + val);
        //Serial.write("to: " + adr);
        //Serial.write("\n");
        EEPROM.write(adr,val);
    }
    if (parts[0] == "PE") { // PEEK
        int adr = parts[1].toInt();
        int val = EEPROM.read(adr);
        Serial.write("PEEK @");
        Serial.println(adr);
        Serial.write(": ");
        Serial.println(val);
    }
    
}

void loadKeyboardArrays() {
    for (int v = 0; v < 8; v++) {
        init_NES_btns[v] = EEPROM.read(v);
    }
    for (int ed = 8; ed < 20; ed++) {
        init_SNES_btns[ed - 8] = EEPROM.read(ed);
    }
    for (int df = 18; df < 36; df++) {
        init_N64_btns[df - 18] = EEPROM.read(df);
    }
}

void checkButton(int button) {

    int button_index = button - 1;
      
    if (button_index > 11) return; // if we have passed all the buttons just return

    if (outputMode == 1) {
        // if in nesMode and button index < 8 then according to the inversion of the state of dataPinNes press or release a button
        if ((modeSelect == 1) && button_index < 8) {
            if(!digitalRead(dataPinNes)) { 
                Keyboard.press(tolower(init_NES_btns[button_index]));
                held_NES_btns[button_index] = init_NES_btns[button_index]; // add the button to the held array
            }
            else {
                for(int op; op < 8; op++ ) { // check if the button is curretnyl in the held array
                    if (op == button_index ){ // We do not care if the button currently being checked has the key held.
                        continue; // so skip the check
                    }
                    if (held_NES_btns[op] == init_NES_btns[button_index]) {
                        held_NES_btns[button_index] = 0; // Do reset the hold
                        return; // if it is, return without unpressing the key
                    }
                }
                // if not, unpress the key 
                Keyboard.release(tolower(init_NES_btns[button_index]));
                held_NES_btns[button_index] = 0; // then remove it from the held array
            }
        }
        // if in snesMode then according to the inversion of the state of dataPinSnes press or release a button
        if ((modeSelect == 2)) {
            if(!digitalRead(dataPinSnes)) { 
                Keyboard.press(tolower(init_SNES_btns[button_index]));
                held_SNES_btns[button_index] = init_SNES_btns[button_index]; // add the button to the held array
            }
            else {
                for(int op; op < 12; op++ ) { // check if the button is curretnyl in the held array
                    if (op == button_index ){ // We do not care if the button currently being checked has the key held.
                        continue; // so skip the check
                    }
                    if (held_SNES_btns[op] == init_SNES_btns[button_index]) {
                        held_SNES_btns[button_index] = 0; // Do reset the hold
                        return; // if it is, return without unpressing the key
                    }
                }
                // if not, unpress the key 
                Keyboard.release(tolower(init_SNES_btns[button_index]));
                held_SNES_btns[button_index] = 0; // then remove it from the held array
            }
        }
        return;
    }
    
    if (outputMode == 0) {
        if ((modeSelect == 1) && button_index < 8) {
            if (button_index > 3) {
                if ((button_index == 4) && (digitalRead(dataPinNes) == HIGH)) { // Try to stop holding up
                    if (holdCheckUpDown == 1) { // check if we are holding up
                        usbStick.setYAxis(ANALOG_IDLE_VALUE); // release up
                        holdCheckUpDown = 0; // Let us know it's no longer held
                    } 
                }
                if ((button_index == 5) && (digitalRead(dataPinNes) == HIGH)) { // Try to stop holding down
                    if (holdCheckUpDown == 2) { // check if we are holding down
                        usbStick.setYAxis(ANALOG_IDLE_VALUE); // release down
                        holdCheckUpDown = 0; // Let us know it's no longer held
                    } 
                }
                if ((button_index == 4) && (digitalRead(dataPinNes) == LOW)) { //if 4th and read data from dataPin
                    usbStick.setYAxis(ANALOG_MIN_VALUE); // press up
                    holdCheckUpDown = 1; // we are holding up
                }
                if ((button_index == 5) && (digitalRead(dataPinNes) == LOW)) { //if 5th and read data from dataPin
                    usbStick.setYAxis(ANALOG_MAX_VALUE); // press down
                    holdCheckUpDown = 2; // we are holding down
                }


                if ((button_index == 6) && (digitalRead(dataPinNes) == HIGH)) { // Try to stop holding left
                    if (holdCheckLeftRight == 1) { // check if we are holding left
                        usbStick.setXAxis(ANALOG_IDLE_VALUE); // release left
                        holdCheckLeftRight = 0; // Let us know it's no longer held
                    } 
                }
                if ((button_index == 7) && (digitalRead(dataPinNes) == HIGH)) { // Try to stop holding right
                    if (holdCheckLeftRight == 2) { // check if we are holding right
                        usbStick.setXAxis(ANALOG_IDLE_VALUE); // release
                        holdCheckLeftRight = 0; // Let us know it's no longer held
                    } 
                }
                if ((button_index == 6) && (digitalRead(dataPinNes) == LOW)) { //if 6th and read data from dataPin
                    usbStick.setXAxis(ANALOG_MIN_VALUE); // press left
                    holdCheckLeftRight = 1; // we are holding left
                }
                if ((button_index == 7) && (digitalRead(dataPinNes) == LOW)) { //if 7th and read data from dataPin
                    usbStick.setXAxis(ANALOG_MAX_VALUE); // press right
                    holdCheckLeftRight = 2; // we are holding right
                }
                usbStick.sendState();
                return;
            }

            usbStick.setButton(controllerbutton_values[button_index], !digitalRead(dataPinNes)); // do the button at the index according to datapin read
            usbStick.sendState();
            return;
        }

        if (modeSelect == 2) {
            if (button_index > 3 && button_index < 8) {
                if ((button_index == 4) && (digitalRead(dataPinSnes) == HIGH)) { // Try to stop holding up
                    if (holdCheckUpDown == 1) { // check if we are holding up
                        usbStick.setYAxis(ANALOG_IDLE_VALUE); // release up
                        holdCheckUpDown = 0; // Let us know it's no longer held
                    } 
                }
                if ((button_index == 5) && (digitalRead(dataPinSnes) == HIGH)) { // Try to stop holding down
                    if (holdCheckUpDown == 2) { // check if we are holding down
                        usbStick.setYAxis(ANALOG_IDLE_VALUE); // release down
                        holdCheckUpDown = 0; // Let us know it's no longer held
                    } 
                }
                if ((button_index == 4) && (digitalRead(dataPinSnes) == LOW)) { //if 4th and read data from dataPin
                    usbStick.setYAxis(ANALOG_MIN_VALUE); // press up
                    holdCheckUpDown = 1; // we are holding up
                }
                if ((button_index == 5) && (digitalRead(dataPinSnes) == LOW)) { //if 5th and read data from dataPin
                    usbStick.setYAxis(ANALOG_MAX_VALUE); // press down
                    holdCheckUpDown = 2; // we are holding down
                }

                if ((button_index == 6) && (digitalRead(dataPinSnes) == HIGH)) { // Try to stop holding left
                    if (holdCheckLeftRight == 1) { // check if we are holding left
                        usbStick.setXAxis(ANALOG_IDLE_VALUE); // release left
                        holdCheckLeftRight = 0; // Let us know it's no longer held
                    } 
                }
                if ((button_index == 7) && (digitalRead(dataPinSnes) == HIGH)) { // Try to stop holding right
                    if (holdCheckLeftRight == 2) { // check if we are holding right
                        usbStick.setXAxis(ANALOG_IDLE_VALUE); // release
                        holdCheckLeftRight = 0; // Let us know it's no longer held
                    }  
                }
                if ((button_index == 6) && (digitalRead(dataPinSnes) == LOW)) { //if 6th and read data from dataPin
                    usbStick.setXAxis(ANALOG_MIN_VALUE); // press left
                    holdCheckLeftRight = 1; // we are holding left
                }
                if ((button_index == 7) && (digitalRead(dataPinSnes) == LOW)) { //if 7th and read data from dataPin
                    usbStick.setXAxis(ANALOG_MAX_VALUE); // press right
                    holdCheckLeftRight = 2; // we are holding right
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

}

void clearAllButtons() {
    for(int sdv = 0; sdv < 8; sdv++) {
        held_NES_btns[sdv] = 0;
    }
    for(int sdv = 0; sdv < 12; sdv++) {
        held_SNES_btns[sdv] = 0;
    }
    for(int sdv = 0; sdv < 18; sdv++) {
        held_N64_btns[sdv] = 0;
    }
    usbStick.setYAxis(ANALOG_IDLE_VALUE); // Clear the Dpad
    usbStick.setXAxis(ANALOG_IDLE_VALUE); // Clear the Dpad
    for(int u = 0; u < 12; u++) usbStick.setButton(controllerbutton_values[u], 0); // Clear the buttons
    usbStick.sendState();
    Keyboard.releaseAll(); // release all keys
}

void nes() {
    nesControllerConnectedLast = nesControllerConnected; // Store the last known state of of the controllers connection

    digitalWrite(latchPinNes, HIGH);
    if (nesControllerConnected && (modeSelect == 1)) checkButton(1); // check for A here

    delayMicroseconds(12);

    digitalWrite(latchPinNes, LOW);
    delayMicroseconds(6);
    

    for (int j = 2; j < 9; j++)
    {
        // pulse the pulse pin

        //6 high
        digitalWrite(pulsePinNes, HIGH);
        // check for the rest of the buttons
        if (nesControllerConnected && (modeSelect == 1)) checkButton(j); 
        delayMicroseconds(6);

        //6 low
        digitalWrite(pulsePinNes, LOW);
        delayMicroseconds(6);
    }

    for (int t = 8; t < 17; t++) {
        // pulse the pulse pin
        //6 high
        digitalWrite(pulsePinNes, HIGH);
        digitalRead(dataPinNes) ? nesControllerConnected = false : nesControllerConnected = true; // If data pin is high here, a controller is connected.
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
    if (snesControllerConnected && (modeSelect == 2)) checkButton(1); // first button, only read a button input if a controller is connected

    delayMicroseconds(12);

    digitalWrite(latchPinSnes, LOW);
    delayMicroseconds(6);
    
    // Pulse genreation and read for rest of buttons
    for (int j = 2; j < 13; j++)
    {
        // pulse the pulse pin

        //6 high
        digitalWrite(pulsePinSnes, HIGH);   
        if (snesControllerConnected && (modeSelect == 2)) checkButton(j); // check for the rest of the buttons, only read a button input if a controller is connected
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
    static boolean haveController = false;
	if (!haveController) {
		if (pad.begin ()) {
			// Controller detected!
				digitalWrite (LED_BUILTIN, HIGH);
				haveController = true;
			} else {
				delay (333);
		}
	} else {
		if (!pad.read ()) {
			// Controller lost :(
			digitalWrite (LED_BUILTIN, LOW);
			haveController = false;
		} else {
            if (outputMode == 0) {
                // map to controller
                // Controller was read fine
			    // Map buttons!
			    usbStick.setButton (0, (pad.buttons & N64Pad::BTN_B) != 0);
			    usbStick.setButton (1, (pad.buttons & N64Pad::BTN_A) != 0);
			    usbStick.setButton (2, (pad.buttons & N64Pad::BTN_C_LEFT) != 0);
			    usbStick.setButton (3, (pad.buttons & N64Pad::BTN_C_DOWN) != 0);
			    usbStick.setButton (4, (pad.buttons & N64Pad::BTN_C_UP) != 0);
			    usbStick.setButton (5, (pad.buttons & N64Pad::BTN_C_RIGHT) != 0);
			    usbStick.setButton (6, (pad.buttons & N64Pad::BTN_L) != 0);
			    usbStick.setButton (7, (pad.buttons & N64Pad::BTN_R) != 0);
			    usbStick.setButton (8, (pad.buttons & N64Pad::BTN_Z) != 0);
			    usbStick.setButton (9, (pad.buttons & N64Pad::BTN_START) != 0);

			    	// D-Pad makes up the X/Y axes
			    	if ((pad.buttons & N64Pad::BTN_UP) != 0) {
			    		usbStick.setYAxis (ANALOG_MIN_VALUE);
			    	} else if ((pad.buttons & N64Pad::BTN_DOWN) != 0) {
			    		usbStick.setYAxis (ANALOG_MAX_VALUE);
			    	} else {
			    		usbStick.setYAxis (ANALOG_IDLE_VALUE);
			    	}
			    	if ((pad.buttons & N64Pad::BTN_LEFT) != 0) {
			    		usbStick.setXAxis (ANALOG_MIN_VALUE);
			    	} else if ((pad.buttons & N64Pad::BTN_RIGHT) != 0) {
			    		usbStick.setXAxis (ANALOG_MAX_VALUE);
			    	} else {
			    		usbStick.setXAxis (ANALOG_IDLE_VALUE);
			    	}
			    	// The analog stick gets mapped to the X/Y rotation axes
			    	usbStick.setRxAxis (pad.x);
			    	usbStick.setRyAxis (pad.y);
			    // All done, send data for real!
			    usbStick.sendState ();
			    
            }
            else {
                //map to keyboard
                // Map buttons!
                
                ((pad.buttons & N64Pad::BTN_A) != 0) ? Keyboard.press(tolower(init_N64_btns[0])) : Keyboard.release(tolower(init_N64_btns[0]));
                ((pad.buttons & N64Pad::BTN_B) != 0) ? Keyboard.press(tolower(init_N64_btns[1])) : Keyboard.release(tolower(init_N64_btns[1]));
                ((pad.buttons & N64Pad::BTN_Z) != 0) ? Keyboard.press(tolower(init_N64_btns[2])) : Keyboard.release(tolower(init_N64_btns[2]));
                ((pad.buttons & N64Pad::BTN_START) != 0) ? Keyboard.press(tolower(init_N64_btns[3])) : Keyboard.release(tolower(init_N64_btns[3]));
                if ((pad.buttons & N64Pad::BTN_UP) == 0) { // Try to stop holding up
                    if (holdCheckUpDown == 1) { // check if we are holding up
                        Keyboard.release(tolower(init_N64_btns[4])); // release up
                        holdCheckUpDown = 0; // Let us know it's no longer held
                    } 
                }
                if ((pad.buttons & N64Pad::BTN_DOWN) == 0) { // Try to stop holding down
                    if (holdCheckUpDown == 2) { // check if we are holding down
                        Keyboard.release(tolower(init_N64_btns[5])); // release down
                        holdCheckUpDown = 0; // Let us know it's no longer held
                    } 
                }
                if ((pad.buttons & N64Pad::BTN_UP) != 0) { //if 4th and read data from dataPin
                    Keyboard.press(tolower(init_N64_btns[4])); // press up
                    holdCheckUpDown = 1; // we are holding up
                }
                if ((pad.buttons & N64Pad::BTN_DOWN) != 0) { //if 5th and read data from dataPin
                    Keyboard.press(tolower(init_N64_btns[5])); // press down
                    holdCheckUpDown = 2; // we are holding down
                }


                if ((pad.buttons & N64Pad::BTN_LEFT) == 0) { // Try to stop holding left
                    if (holdCheckLeftRight == 1) { // check if we are holding left
                        Keyboard.release(tolower(init_N64_btns[6])); // release left
                        holdCheckLeftRight = 0; // Let us know it's no longer held
                    } 
                }
                if ((pad.buttons & N64Pad::BTN_RIGHT) == 0) { // Try to stop holding right
                    if (holdCheckLeftRight == 2) { // check if we are holding right
                        Keyboard.release(tolower(init_N64_btns[7])); // release
                        holdCheckLeftRight = 0; // Let us know it's no longer held
                    } 
                }
                if ((pad.buttons & N64Pad::BTN_LEFT) != 0) { //if 6th and read data from dataPin
                    Keyboard.press(tolower(init_N64_btns[6])); // press left
                    holdCheckLeftRight = 1; // we are holding left
                }
                if ((pad.buttons & N64Pad::BTN_RIGHT) != 0) { //if 7th and read data from dataPin
                    Keyboard.press(tolower(init_N64_btns[7])); // press right
                    holdCheckLeftRight = 2; // we are holding right
                }
                ((pad.buttons & N64Pad::BTN_C_DOWN) != 0) ? Keyboard.press(tolower(init_N64_btns[8])) : Keyboard.release(tolower(init_N64_btns[8]));
                ((pad.buttons & N64Pad::BTN_C_LEFT) != 0) ? Keyboard.press(tolower(init_N64_btns[9])) : Keyboard.release(tolower(init_N64_btns[9]));
                ((pad.buttons & N64Pad::BTN_L) != 0) ? Keyboard.press(tolower(init_N64_btns[10])) : Keyboard.release(tolower(init_N64_btns[10]));
                ((pad.buttons & N64Pad::BTN_R) != 0) ? Keyboard.press(tolower(init_N64_btns[11])) : Keyboard.release(tolower(init_N64_btns[11]));
                ((pad.buttons & N64Pad::BTN_C_UP) != 0) ? Keyboard.press(tolower(init_N64_btns[12])) : Keyboard.release(tolower(init_N64_btns[12]));
                ((pad.buttons & N64Pad::BTN_C_RIGHT) != 0) ? Keyboard.press(tolower(init_N64_btns[13])) : Keyboard.release(tolower(init_N64_btns[13]));
  

                if (pad.y < n64KeyJoystickDeadZone) { // Try to stop holding up
                    if (holdCheckUpDownN64Joy == 1) { // check if we are holding up
                        Keyboard.release(tolower(init_N64_btns[14])); // release up
                        holdCheckUpDownN64Joy = 0; // Let us know it's no longer held
                    } 
                }
                if (pad.y > (-1 * n64KeyJoystickDeadZone)) { // Try to stop holding down
                    if (holdCheckUpDownN64Joy == 2) { // check if we are holding down
                        Keyboard.release(tolower(init_N64_btns[15])); // release down
                        holdCheckUpDownN64Joy = 0; // Let us know it's no longer held
                    } 
                }
                if (pad.y > n64KeyJoystickDeadZone) { //if 4th and read data from dataPin
                    Keyboard.press(tolower(init_N64_btns[14])); // press up
                    holdCheckUpDownN64Joy = 1; // we are holding up
                }
                if (pad.y < (-1 * n64KeyJoystickDeadZone)) { //if 5th and read data from dataPin
                    Keyboard.press(tolower(init_N64_btns[15])); // press down
                    holdCheckUpDownN64Joy = 2; // we are holding down
                }


                if (pad.x < n64KeyJoystickDeadZone) { // Try to stop holding left
                    if (holdCheckLeftRightN64Joy == 1) { // check if we are holding left
                        Keyboard.release(tolower(init_N64_btns[17])); // release left
                        holdCheckLeftRightN64Joy = 0; // Let us know it's no longer held
                    } 
                }
                if (pad.x > (-1 * n64KeyJoystickDeadZone)) { // Try to stop holding right
                    if (holdCheckLeftRightN64Joy == 2) { // check if we are holding right
                        Keyboard.release(tolower(init_N64_btns[16])); // release
                        holdCheckLeftRightN64Joy = 0; // Let us know it's no longer held
                    } 
                }
                if (pad.x > n64KeyJoystickDeadZone) { //if 6th and read data from dataPin
                    Keyboard.press(tolower(init_N64_btns[17])); // press left
                    holdCheckLeftRightN64Joy = 1; // we are holding left
                }
                if (pad.x < (-1 * n64KeyJoystickDeadZone)) { //if 7th and read data from dataPin
                    Keyboard.press(tolower(init_N64_btns[16])); // press right
                    holdCheckLeftRightN64Joy = 2; // we are holding right
                }               
            }
		}
    } 
}

void checkSwitches() {
  
    if (!digitalRead(serialSwitch)) {
        modeSelect = 0;
    }                                                        
    else if (!digitalRead(nesSwitch)) {
        modeSelect = 1;
    }
    else if (!digitalRead(snesSwitch)) {
        modeSelect = 2;
    }
    else if (!digitalRead(n64Switch)) {
        modeSelect = 3;
    }
    else {//error state 
        modeSelect = 0;
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
    pinMode(outModeSwitch, INPUT_PULLUP);

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
    loadKeyboardArrays();
    //!outModeSwitch ? outputMode = 1 : outputMode = 0;
    //(snesControllerConnected || nesControllerConnected) ? digitalWrite (LED_BUILTIN, HIGH): digitalWrite (LED_BUILTIN, LOW);

    checkSwitches();
    if( modeSelectLast != modeSelect) {
        clearAllButtons();
    }
    modeSelectLast = modeSelect;

    if (modeSelect == 0) serialActions();
    if (modeSelect == 1) nes();
    if (modeSelect == 2) snes();
    if (modeSelect == 3) n64();
         
}