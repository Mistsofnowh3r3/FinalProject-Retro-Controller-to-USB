

#include <N64Pad.h> // https://github.com/SukkoPera/N64PadForArduino
#include <Joystick.h> // https://github.com/MHeironimus/ArduinoJoystickLibrary
#include <Keyboard.h> // https://github.com/arduino-libraries/Keyboard
#include <EEPROM.h>
#include <stdio.h>

#pragma region // EEPROM MEMORY MAP
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
#pragma endregion

#pragma region // Variables

// Globals //

String serialNow = ""; // Will hold the current text in the serial line

int init_NES_btns[] = { // Holds the current keyboard key mappings
    'z',            //A
    'x',            //B
    KEY_RETURN,     //SELECT
    32,             //START
    KEY_UP_ARROW,   //UP   
    KEY_DOWN_ARROW, //DOWN
    KEY_LEFT_ARROW, //LEFT
    KEY_RIGHT_ARROW,//RIGHT
};
int held_NES_btns[8]; // Holds the current held keyboard keys

int init_SNES_btns[] = { // Holds the current keyboard key mappings
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
int held_SNES_btns[12]; // Holds the current held keyboard keys

int init_N64_btns[] = { // Holds the current keyboard key mappings
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
int held_N64_btns[18]; // Holds the current held keyboard keys

int controllerbutton_values[] = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11}; // Array of the controller buttons
int n64KeyJoystickDeadZone = 20; // Deadzone for converting N64 joystick to keyboard key press

// Used for making sure that the DPAD functions correctly.
int holdCheckUpDown = 5;
int holdCheckLeftRight = 5;
int holdCheckUpDownN64Joy = 5;
int holdCheckLeftRightN64Joy = 5;

//various mode related things
// 0 serial, 1 NES, 2 SNES, 3 N64
int modeSelect = 0;
int modeSelectLast = 4;

// State of controller connections
bool nesControllerConnected = false;
bool nesControllerConnectedLast = false; 
bool snesControllerConnected = false; 
bool snesControllerConnectedLast = false; 
bool n64ControllerConnected = false;

// Controller/keyboard output select 
//  0 = Controller 1 = Keyboard 
bool outputMode = 0;
bool outputModelast = 0;

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
// n64 data pin is 3, defined in n64pad code
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

const int outModeSwitch = PIN_A5; // Keyboard or Controller mode switch

#pragma endregion


// Handles the Arduinos actions in serial mode.
void serialActions() {
    clearAllButtons(); // Assures no buttons are currently being sent
    
    String parts[4]; // Create an array to hold 4 parts
    memset(parts, 0, sizeof(parts));  // Clear the parts array
    serialNow = Serial.readStringUntil('!');  // read the command

    int partIndex = 0; // Initialize the array index
    for (int i = 0; i < serialNow.length(); i++) {
        char c = serialNow.charAt(i); // Get the character at i in serialNow 
        if (c == ',') partIndex++;  // If the character is a comma, move to the next substring in parts
        else parts[partIndex] += c; // Else, at the character to the current substring in parts
    }

    // POKE command
    // PO, ADR, VAL, CONSOLE !
    if (parts[0] == "PO") { 
        int adr = parts[1].toInt();
        int val = parts[2].toInt();

        if(parts[3] == "SNES") {
            adr += 8; // Add 8 to the address if SNES, makes sure we are at the SNES mapping memory address
        }
        else if(parts[3] == "N64") {
            adr += 18; // Add 18 to the address if N64, makes sure we are at the N64 mapping memory address
        }
        EEPROM.write(adr,val); // Write the value given at the address given
    }

    // PEEK command
    // PO, ADR, VAL, CONSOLE !
    if (parts[0] == "PE") { 
        int adr = parts[1].toInt();
        int val = EEPROM.read(adr);
        // Write the value at the address to the serial
        Serial.write("PEEK @");
        Serial.println(adr);
        Serial.write(": ");
        Serial.println(val);
    }
    
}

// Reads from EEPROM to load the stored keyboard mappings
void loadKeyboardArrays() {
    for (int gamer = 0; gamer < 36; gamer++) {
        if ( gamer < 8) init_NES_btns[gamer] = EEPROM.read(gamer);
        if ( gamer > 7 && gamer < 18) init_SNES_btns[gamer - 8] = EEPROM.read(gamer);
        if ( gamer > 17) init_N64_btns[gamer - 18] = EEPROM.read(gamer);
    }
}

// Button press handling for NES and SNES controllers
void checkButton(int button) {

    int button_index = button - 1;
      
    if (button_index > 11) return; // if we have passed all the buttons just return

    if (outputMode == 1) { // If in keyboard mode
        // If in nesMode and button index < 8 then according to the inversion of the state of dataPinNes press or release a button
        if ((modeSelect == 1) && button_index < 8) {
            if(!digitalRead(dataPinNes)) { 
                Keyboard.press(tolower(init_NES_btns[button_index]));
                held_NES_btns[button_index] = init_NES_btns[button_index]; // Add the button to the held array
            }
            else {
                for(int op; op < 8; op++ ) { // Check if the button is currently in the held array
                    if (op == button_index ){ // Do not check for the current key being checked
                        continue; 
                    }
                    if (held_NES_btns[op] == init_NES_btns[button_index]) { // If the key is in the held array
                        held_NES_btns[button_index] = 0; // Remove it from the held array
                        return; // But return so the key does not get released
                    }
                }
                // Finally, if the key is not on the held array.
                Keyboard.release(tolower(init_NES_btns[button_index])); // Release it
                held_NES_btns[button_index] = 0; // Then remove it from the held array   
            }
            return;
        }
        // If in snesMode then according to the inversion of the state of dataPinSnes press or release a button
        if ((modeSelect == 2)) {
            if(!digitalRead(dataPinSnes)) { 
                Keyboard.press(tolower(init_SNES_btns[button_index]));
                held_SNES_btns[button_index] = init_SNES_btns[button_index]; // Add the button to the held array
            }
            else {
                for(int op; op < 12; op++ ) { // Check if the button is currently in the held array
                    if (op == button_index ){ // Do not check for the current key being checked
                        continue;
                    }
                    if (held_SNES_btns[op] == init_SNES_btns[button_index]) { // If the key is in the held array
                        held_SNES_btns[button_index] = 0; // Remove it from the held array
                        return; // But return so the key does not get released
                    }
                }
                // Finally, if the key is not on the held array.
                Keyboard.release(tolower(init_SNES_btns[button_index])); // Release it
                held_SNES_btns[button_index] = 0; // Then remove it from the held array
            }
        }
        return;
    }
    
    if (outputMode == 0) { // If in controller mode
        if ((modeSelect == 1) && button_index < 8) { // If in NES mode and not past the 8th button
            if (button_index > 3) {
                if ((button_index == 4) && (digitalRead(dataPinNes) == HIGH)) { // Try to stop holding up
                    if (holdCheckUpDown == 1) { // Check if we are holding up
                        usbStick.setYAxis(ANALOG_IDLE_VALUE); // Release up
                        holdCheckUpDown = 0; // Let us know up is no longer held
                    } 
                }
                if ((button_index == 5) && (digitalRead(dataPinNes) == HIGH)) { // Try to stop holding down
                    if (holdCheckUpDown == 2) { // Check if we are holding down
                        usbStick.setYAxis(ANALOG_IDLE_VALUE); // Release down
                        holdCheckUpDown = 0; // Let us know down is no longer held
                    } 
                }
                if ((button_index == 4) && (digitalRead(dataPinNes) == LOW)) { // Try to press up
                    usbStick.setYAxis(ANALOG_MIN_VALUE); // Press up
                    holdCheckUpDown = 1; // We are holding up
                }
                if ((button_index == 5) && (digitalRead(dataPinNes) == LOW)) { // Try to press down
                    usbStick.setYAxis(ANALOG_MAX_VALUE); // Press down
                    holdCheckUpDown = 2; // We are holding down
                }


                if ((button_index == 6) && (digitalRead(dataPinNes) == HIGH)) { // Try to stop holding left
                    if (holdCheckLeftRight == 1) { // Check if we are holding left
                        usbStick.setXAxis(ANALOG_IDLE_VALUE); // Release left
                        holdCheckLeftRight = 0; // Let us know left is no longer held
                    } 
                }
                if ((button_index == 7) && (digitalRead(dataPinNes) == HIGH)) { // Try to stop holding right
                    if (holdCheckLeftRight == 2) { // Check if we are holding right
                        usbStick.setXAxis(ANALOG_IDLE_VALUE); // Release
                        holdCheckLeftRight = 0; // Let us know right is no longer held
                    } 
                }
                if ((button_index == 6) && (digitalRead(dataPinNes) == LOW)) { // Try to press left
                    usbStick.setXAxis(ANALOG_MIN_VALUE); // Press left
                    holdCheckLeftRight = 1; // We are holding left
                }
                if ((button_index == 7) && (digitalRead(dataPinNes) == LOW)) { // Try to press right
                    usbStick.setXAxis(ANALOG_MAX_VALUE); // Press right
                    holdCheckLeftRight = 2; // We are holding right
                }
                usbStick.sendState(); // Send the controller state
                return; // Return to not mess stuff up
            }
            // Press the button at the index according to the datapin read
            usbStick.setButton(controllerbutton_values[button_index], !digitalRead(dataPinNes)); 
            usbStick.sendState();
            return;
        }

        if (modeSelect == 2) { // If in SNES mode
            if (button_index > 3 && button_index < 8) {
                if ((button_index == 4) && (digitalRead(dataPinSnes) == HIGH)) { // Try to stop holding up
                    if (holdCheckUpDown == 1) { // Check if we are holding up
                        usbStick.setYAxis(ANALOG_IDLE_VALUE); // Release up
                        holdCheckUpDown = 0; // Let us know up is no longer held
                    } 
                }
                if ((button_index == 5) && (digitalRead(dataPinSnes) == HIGH)) { // Try to stop holding down
                    if (holdCheckUpDown == 2) { // Check if we are holding down
                        usbStick.setYAxis(ANALOG_IDLE_VALUE); // Release down
                        holdCheckUpDown = 0; // Let us know down is no longer held
                    } 
                }
                if ((button_index == 4) && (digitalRead(dataPinSnes) == LOW)) { // Try to press up
                    usbStick.setYAxis(ANALOG_MIN_VALUE); // Press up
                    holdCheckUpDown = 1; // We are holding up
                }
                if ((button_index == 5) && (digitalRead(dataPinSnes) == LOW)) { // Try to press down
                    usbStick.setYAxis(ANALOG_MAX_VALUE); // Press down
                    holdCheckUpDown = 2; // We are holding down
                }

                if ((button_index == 6) && (digitalRead(dataPinSnes) == HIGH)) { // Try to stop holding left
                    if (holdCheckLeftRight == 1) { // Check if we are holding left
                        usbStick.setXAxis(ANALOG_IDLE_VALUE); // Release left
                        holdCheckLeftRight = 0; // Let us know left is no longer held
                    } 
                }
                if ((button_index == 7) && (digitalRead(dataPinSnes) == HIGH)) { // Try to stop holding right
                    if (holdCheckLeftRight == 2) { // Check if we are holding right
                        usbStick.setXAxis(ANALOG_IDLE_VALUE); // Release right
                        holdCheckLeftRight = 0; // Let us know right is no longer held
                    }  
                }
                if ((button_index == 6) && (digitalRead(dataPinSnes) == LOW)) { // Try to press left
                    usbStick.setXAxis(ANALOG_MIN_VALUE); // Press left
                    holdCheckLeftRight = 1; // We are holding left
                }
                if ((button_index == 7) && (digitalRead(dataPinSnes) == LOW)) { // Try to press right
                    usbStick.setXAxis(ANALOG_MAX_VALUE); // Press right
                    holdCheckLeftRight = 2; // We are holding right
                }
                usbStick.sendState(); // Send the controller state
                return; // Return to not mess stuff up
            }
            // Press the button at the index according to the datapin read
            usbStick.setButton(controllerbutton_values[button_index], !digitalRead(dataPinSnes)); 
            usbStick.sendState(); 
        }
    }
}

// Releases all buttons and keyboard keys
void clearAllButtons() {
    for (int gamer = 0; gamer < 36; gamer++) {
        if ( gamer < 8) held_NES_btns[gamer] = 0;
        if ( gamer > 7 && gamer < 18) held_SNES_btns[gamer - 8] = 0;
        if ( gamer > 17) held_N64_btns[gamer - 18] = 0;
    }
    usbStick.setYAxis(ANALOG_IDLE_VALUE); // Clear the Dpad
    usbStick.setXAxis(ANALOG_IDLE_VALUE); // Clear the Dpad
    usbStick.setRyAxis(ANALOG_IDLE_VALUE); // Clear the joystick
    usbStick.setRxAxis(ANALOG_IDLE_VALUE); // Clear the joystick
    for(int u = 0; u < 12; u++) usbStick.setButton(controllerbutton_values[u], 0); // Clear the buttons
    usbStick.sendState();
    Keyboard.releaseAll(); // Release all keys
}

// Contructs the timing for supplying pulse and clock to the NES controller, and to read the values at it's dataPin at the right time
void nes() {
    nesControllerConnectedLast = nesControllerConnected; // Store the last known state of of the controllers connection

    digitalWrite(latchPinNes, HIGH); // Write high to the latchPin
    if (nesControllerConnected && (modeSelect == 1)) checkButton(1); // Check for A here
    delayMicroseconds(12); // Keep latchPin high for 12 ms

    digitalWrite(latchPinNes, LOW); // Write low to the latchPin
    delayMicroseconds(6); // Wait 6 ms
    

    for (int j = 2; j < 9; j++) // Create 7 pulses on the pulse pin
    {
        digitalWrite(pulsePinNes, HIGH); // Write high to the pulsePin
        if (nesControllerConnected && (modeSelect == 1)) checkButton(j); // Check for all other buttons
        delayMicroseconds(6); // Keep pulsePin high for 6 ms

        digitalWrite(pulsePinNes, LOW); // Write low to the pulsePin
        delayMicroseconds(6); // Keep pulsePin low for 6 ms
    }

    for (int t = 8; t < 17; t++) { // Extra pulses for checking if an NES controller is connected
        digitalWrite(pulsePinNes, HIGH); // Write high to the pulsePin
        digitalRead(dataPinNes) ? nesControllerConnected = false : nesControllerConnected = true; // If data pin is high here, a controller is connected.
        delayMicroseconds(6); // Keep pulsePin high for 6 ms
        digitalWrite(pulsePinNes, LOW); // Write low to the pulsePin
        delayMicroseconds(6);
    }
    

    if(!nesControllerConnected && (nesControllerConnected != nesControllerConnectedLast)) { // if a NES controller is not connected, and the connection has changed 
        clearAllButtons(); // need to only do this when the controller actually becomes disconnected
    }
}   

// Contructs the timing for supplying pulse and clock to the SNES controller, and to read the values at it's dataPin at the right time
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

void n64KeyPress(bool check, int button)
{
    if(check) { // Check if the button was pressed 
        Keyboard.press(tolower(init_N64_btns[button])); // if so press it
        held_N64_btns[button] = init_N64_btns[button]; // add the button to the held array
    }
    else { //or released
        for(int op; op < 18; op++ ) { // check if the button is curretnyl in the held array
            if (op == button ){ // We do not care if the button currently being checked has the key held.
                continue; // so skip the check
            }
            if (held_N64_btns[op] == init_N64_btns[button]) {
                held_N64_btns[button] = 0; // Do reset the hold but don't unpress
                return; // if it is, return without unpressing the key
            }
        }
        // if not, unpress the key 
        Keyboard.release(tolower(init_N64_btns[button]));
        held_N64_btns[button] = 0; // then remove it from the held array
    }
}

void n64() {
    static boolean haveController = false;
	if (!haveController) {
		if (pad.begin ()) {
			// Controller detected!
                digitalWrite (LED_BUILTIN, HIGH);
				n64ControllerConnected = true;
				haveController = true;
			} else {
				//delay (333);
		}
	} else {
		if (!pad.read ()) {
			// Controller lost :(
            digitalWrite (LED_BUILTIN, LOW);
			n64ControllerConnected = false;
			haveController = false;
		} else if (modeSelect == 3) {
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
                
                n64KeyPress((pad.buttons & N64Pad::BTN_A) != 0, 0);
                n64KeyPress((pad.buttons & N64Pad::BTN_B) != 0, 1);
                n64KeyPress((pad.buttons & N64Pad::BTN_Z) != 0, 2);
                n64KeyPress((pad.buttons & N64Pad::BTN_START) != 0, 3);

                if ((pad.buttons & N64Pad::BTN_UP) == 0) { // Try to stop holding up
                    if (holdCheckUpDown == 1) { // check if we are holding up
                        n64KeyPress(false, 4); // release up
                        holdCheckUpDown = 0; // Let us know it's no longer held
                    } 
                }
                if ((pad.buttons & N64Pad::BTN_DOWN) == 0) { // Try to stop holding down
                    if (holdCheckUpDown == 2) { // check if we are holding down
                        n64KeyPress(false, 5); // release down
                        holdCheckUpDown = 0; // Let us know it's no longer held
                    } 
                }
                if ((pad.buttons & N64Pad::BTN_UP) != 0) { //if 4th and read data from dataPin
                    n64KeyPress(true, 4); // press up
                    holdCheckUpDown = 1; // we are holding up
                }
                if ((pad.buttons & N64Pad::BTN_DOWN) != 0) { //if 5th and read data from dataPin
                    n64KeyPress(true, 5); // press down
                    holdCheckUpDown = 2; // we are holding down
                }


                if ((pad.buttons & N64Pad::BTN_LEFT) == 0) { // Try to stop holding left
                    if (holdCheckLeftRight == 1) { // check if we are holding left
                        n64KeyPress(false, 6); // release left
                        holdCheckLeftRight = 0; // Let us know it's no longer held
                    } 
                }
                if ((pad.buttons & N64Pad::BTN_RIGHT) == 0) { // Try to stop holding right
                    if (holdCheckLeftRight == 2) { // check if we are holding right
                        n64KeyPress(false, 7); // release
                        holdCheckLeftRight = 0; // Let us know it's no longer held
                    } 
                }
                if ((pad.buttons & N64Pad::BTN_LEFT) != 0) { //if 6th and read data from dataPin
                    n64KeyPress(true, 6); // press left
                    holdCheckLeftRight = 1; // we are holding left
                }
                if ((pad.buttons & N64Pad::BTN_RIGHT) != 0) { //if 7th and read data from dataPin
                    n64KeyPress(true, 7); // press right
                    holdCheckLeftRight = 2; // we are holding right
                }
                
                n64KeyPress((pad.buttons & N64Pad::BTN_C_DOWN) != 0, 8);
                n64KeyPress((pad.buttons & N64Pad::BTN_C_LEFT) != 0, 9);
                n64KeyPress((pad.buttons & N64Pad::BTN_L) != 0, 10);
                n64KeyPress((pad.buttons & N64Pad::BTN_R) != 0, 11);
                n64KeyPress((pad.buttons & N64Pad::BTN_C_UP) != 0, 12);
                n64KeyPress((pad.buttons & N64Pad::BTN_C_RIGHT) != 0, 13);

                if (pad.y < n64KeyJoystickDeadZone) { // Try to stop holding up
                    if (holdCheckUpDownN64Joy == 1) { // check if we are holding up
                        n64KeyPress(false, 14); // release up
                        holdCheckUpDownN64Joy = 0; // Let us know it's no longer held
                    } 
                }
                if (pad.y > (-1 * n64KeyJoystickDeadZone)) { // Try to stop holding down
                    if (holdCheckUpDownN64Joy == 2) { // check if we are holding down
                        n64KeyPress(false, 15); // release down
                        holdCheckUpDownN64Joy = 0; // Let us know it's no longer held
                    } 
                }
                if (pad.y > n64KeyJoystickDeadZone) { //if 4th and read data from dataPin
                    n64KeyPress(true, 14); // press up
                    holdCheckUpDownN64Joy = 1; // we are holding up
                }
                if (pad.y < (-1 * n64KeyJoystickDeadZone)) { //if 5th and read data from dataPin
                    n64KeyPress(true, 15); // press down
                    holdCheckUpDownN64Joy = 2; // we are holding down
                }

                if (pad.x > (-1 * n64KeyJoystickDeadZone)) { // Try to stop holding right
                    if (holdCheckLeftRightN64Joy == 2) { // check if we are holding right
                        n64KeyPress(false, 16); // release
                        holdCheckLeftRightN64Joy = 0; // Let us know it's no longer held
                    } 
                }

                if (pad.x < n64KeyJoystickDeadZone) { // Try to stop holding left
                    if (holdCheckLeftRightN64Joy == 1) { // check if we are holding left
                        n64KeyPress(false, 17); // release left
                        holdCheckLeftRightN64Joy = 0; // Let us know it's no longer held
                    } 
                }

                if (pad.x < (-1 * n64KeyJoystickDeadZone)) { //if 7th and read data from dataPin
                    n64KeyPress(true, 16); // press right
                    holdCheckLeftRightN64Joy = 2; // we are holding right
                } 

                if (pad.x > n64KeyJoystickDeadZone) { //if 6th and read data from dataPin
                    n64KeyPress(true, 17); // press left
                    holdCheckLeftRightN64Joy = 1; // we are holding left
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

void updateLights() {
    
    digitalWrite(nesIndicateLed, nesControllerConnected );
    digitalWrite(snesIndicateLed, snesControllerConnected );
    digitalWrite(n64IndicateLed, n64ControllerConnected );
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

    pinMode(LED_BUILTIN, OUTPUT);

    usbStick.begin (false);		// We'll call sendState() manually to minimize lag. I didn't say this, who else is here?
	usbStick.setXAxisRange (ANALOG_MIN_VALUE, ANALOG_MAX_VALUE);
	usbStick.setYAxisRange (ANALOG_MIN_VALUE, ANALOG_MAX_VALUE);
	usbStick.setRxAxisRange (ANALOG_MIN_VALUE, ANALOG_MAX_VALUE);
	usbStick.setRyAxisRange (ANALOG_MAX_VALUE, ANALOG_MIN_VALUE);
}

void loop() {  
    loadKeyboardArrays();
    
    outputMode = !digitalRead(outModeSwitch);
    if(outputMode != outputModelast) {
        clearAllButtons();
    }
    outputModelast = outputMode;

    checkSwitches();
    if( modeSelectLast != modeSelect) {
        clearAllButtons();
    }
    modeSelectLast = modeSelect;

        updateLights();

    if (modeSelect == 0) serialActions();
    nes();
    snes();
    n64();
         
}