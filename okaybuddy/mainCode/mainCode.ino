#include <N64Pad.h> // https://github.com/SukkoPera/N64PadForArduino
#include <Joystick.h> // https://github.com/MHeironimus/ArduinoJoystickLibrary
#include <Keyboard.h> // https://github.com/arduino-libraries/Keyboard
#include <EEPROM.h> // From Arduino IDE
#include <stdio.h> // From Arduino IDE

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

String serialNow = ""; // Holds the current text in the serial line

int working_NES_btns[] = { // Holds the current keyboard key mappings
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

int working_SNES_btns[] = { // Holds the current keyboard key mappings
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

int working_N64_btns[] = { // Holds the current keyboard key mappings
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

// Keeping track of when to read mappings from the arrays
bool mappingChanged = 0;

// Constants //

// This stuff was in the N64 pad example and was mostly undocumented
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

// Rotary dial pins
// SERIAL | NES | SNES | N64
const int serialDial = 10; 
const int nesDial = 11; 
const int snesDial = 12; 
const int n64Dial = 13; 

// Keyboard or Controller mode switch pin
const int outModeSwitch = PIN_A5; 

#pragma endregion


// Handles the Arduinos actions in serial mode.
void serialActions() {
    
    String parts[4]; // Create an array to hold 4 parts
    memset(parts, 0, sizeof(parts));  // Clear the parts array
    serialNow = Serial.readStringUntil('!');  // Read the command

    int partIndex = 0; // Initialize the array index
    for (int i = 0; i < serialNow.length(); i++) {
        char c = serialNow.charAt(i); // Get the character at i in serialNow 
        if (c == ',') partIndex++;  // If the character is a comma, move to the next substring in parts
        else parts[partIndex] += c; // Else, add the character to the current substring in parts
    }

    // POKE command, writes to a location in EEPROM
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
        EEPROM.write(adr,val); // Write the value given to the address given
        mappingChanged = 1; // Let us know a mapping has changed
    }

    // PEEK command, reads the value at a location in EEPROM
    // PO, ADR, VAL, CONSOLE !
    if (parts[0] == "PE") { 
        int adr = parts[1].toInt();
        int val = EEPROM.read(adr);

        // Write the value at the address to serial
        Serial.write("PEEK @");
        Serial.println(adr);
        Serial.write(": ");
        Serial.println(val);
    }
    
}

// Reads from EEPROM to load the stored keyboard mappings
void loadKeyboardArrays() {
    for (int v = 0; v < 36; v++) {
        if ( v < 8) working_NES_btns[v] = EEPROM.read(v);
        if ( v > 7 && v < 18) working_SNES_btns[v - 8] = EEPROM.read(v);
        if ( v > 17) working_N64_btns[v - 18] = EEPROM.read(v);
    }
}

// Button press handling for NES and SNES controllers
void checkButton(int button_index) {
      
    if (button_index > 11) return; // If we have passed all the buttons just return

    if (outputMode == 1) { // If in keyboard mode
        // If in nesMode and button index < 8 then according to the inversion of the state of dataPinNes press or release a button
        // Does checks to make sure that you can map the same keyboard key to multiple controller buttons, and have it act as expected
        if ((modeSelect == 1) && button_index < 8) {     
            if(!digitalRead(dataPinNes)) { 
                Keyboard.press(tolower(working_NES_btns[button_index])); // tolower ensures the lowercase version of the key is pressed
                held_NES_btns[button_index] = working_NES_btns[button_index]; // Add the button to the held array
            }
            else {
                for(int op; op < 8; op++ ) { // Check if the button is currently in the held array
                    if (op == button_index ){ // Do not check for the current key being checked
                        continue; 
                    }
                    if (held_NES_btns[op] == working_NES_btns[button_index]) { // If the key is in the held array
                        held_NES_btns[button_index] = 0; // Remove it from the held array
                        return; // But return so the key does not get released
                    }
                }
                // Finally, if the key is not on the held array.
                Keyboard.release(tolower(working_NES_btns[button_index])); // Release it
                held_NES_btns[button_index] = 0; // Then remove it from the held array   
            }
            return;
        }
        // If in snesMode then according to the inversion of the state of dataPinSnes press or release a button
        // Does checks to make sure that you can map the same keyboard key to multiple controller buttons, and have it act as expected
        if ((modeSelect == 2)) {    
            if(!digitalRead(dataPinSnes)) { 
                Keyboard.press(tolower(working_SNES_btns[button_index])); // tolower ensures the lowercase version of the key is pressed
                held_SNES_btns[button_index] = working_SNES_btns[button_index]; // Add the button to the held array
            }
            else {
                for(int op; op < 12; op++ ) { // Check if the button is currently in the held array
                    if (op == button_index ){ // Do not check for the current key being checked
                        continue;
                    }
                    if (held_SNES_btns[op] == working_SNES_btns[button_index]) { // If the key is in the held array
                        held_SNES_btns[button_index] = 0; // Remove it from the held array
                        return; // But return so the key does not get released
                    }
                }
                // Finally, if the key is not on the held array.
                Keyboard.release(tolower(working_SNES_btns[button_index])); // Release it
                held_SNES_btns[button_index] = 0; // Then remove it from the held array
            }
        }
        return;
    }
    
    if (outputMode == 0) { // If in controller mode
        if ((modeSelect == 1) && button_index < 8) { // If in NES mode and not past the 8th button
            if (button_index > 3) { // Area of the Dpad buttons
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
            if (button_index > 3 && button_index < 8) { // Area of the Dpad buttons
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
    // Go through all held buttons arrays and initialize them to zero
    for (int v = 0; v < 36; v++) {
        if ( v < 8) held_NES_btns[v] = 0;
        if ( v > 7 && v < 18) held_SNES_btns[v - 8] = 0;
        if ( v > 17) held_N64_btns[v - 18] = 0;
    }
    usbStick.setYAxis(ANALOG_IDLE_VALUE); // Clear the Dpad
    usbStick.setXAxis(ANALOG_IDLE_VALUE); // Clear the Dpad
    usbStick.setRyAxis(ANALOG_IDLE_VALUE); // Clear the joystick
    usbStick.setRxAxis(ANALOG_IDLE_VALUE); // Clear the joystick
    for(int u = 0; u < 12; u++) usbStick.setButton(controllerbutton_values[u], 0); // Clear the buttons
    usbStick.sendState();
    Keyboard.releaseAll(); // Release all keys
}

// Contructs the timing for supplying pulse and clock to the NES controller, and reads the values at it's dataPin at the right time
void nes() {
    nesControllerConnectedLast = nesControllerConnected; // Store the last known state of of the controllers connection

    // NES latch signal generation
    digitalWrite(latchPinNes, HIGH); // Write high to the latchPin
    if (nesControllerConnected && (modeSelect == 1)) checkButton(0); // Check for A here
    delayMicroseconds(12); // Keep latchPin high for 12 ms

    digitalWrite(latchPinNes, LOW); // Write low to the latchPin
    delayMicroseconds(6); // Wait 6 ms
    
    // NES pulse signal generation
    for (int j = 1; j < 8; j++) // Create 7 pulses on the pulse pin
    {
        digitalWrite(pulsePinNes, HIGH); // Write high to the pulsePin
        if (nesControllerConnected && (modeSelect == 1)) checkButton(j); // Check for all other buttons
        delayMicroseconds(6); // Keep pulsePin high for 6 ms

        digitalWrite(pulsePinNes, LOW); // Write low to the pulsePin
        delayMicroseconds(6); // Keep pulsePin low for 6 ms
    }

    for (int t = 7; t < 16; t++) { // Extra pulses for checking if an NES controller is connected
        digitalWrite(pulsePinNes, HIGH); // Write high to the pulsePin
        digitalRead(dataPinNes) ? nesControllerConnected = false : nesControllerConnected = true; // If read low here a controller is connected
        if (nesControllerConnected) {
            digitalWrite(nesIndicateLed, HIGH);
        }
        else {
            digitalWrite(nesIndicateLed, LOW);
        }

        delayMicroseconds(6); // Keep pulsePin high for 6 ms

        digitalWrite(pulsePinNes, LOW); // Write low to the pulsePin
        delayMicroseconds(6); // Keep pulsePin low for 6 ms
    }
    
    // If a NES controller is not connected, and the connection has changed 
    if(!nesControllerConnected && (nesControllerConnected != nesControllerConnectedLast)) { 
        clearAllButtons(); 
    }
}   

// Contructs the timing for supplying pulse and clock to the SNES controller, and reads the values at it's dataPin at the right time
void snes() {
    snesControllerConnectedLast = snesControllerConnected; // Store the last known state of of the controllers connection
    
    // SNES latch signal generation
    digitalWrite(latchPinSnes, HIGH); // Write high to the latchPin
    if (snesControllerConnected && (modeSelect == 2)) checkButton(0); // Check for B here
    delayMicroseconds(12); // Keep latchPin high for 12 ms

    digitalWrite(latchPinSnes, LOW); // Write low to the latchPin
    delayMicroseconds(6); // Wait 6 ms
    
    // SNES pulse signal generation
    for (int j = 1; j < 12; j++) // Create 11 pulses on the pulse pin
    {
        digitalWrite(pulsePinSnes, HIGH); // Write high to the pulsePin
        if (snesControllerConnected && (modeSelect == 2)) checkButton(j); // Check for all other buttons
        delayMicroseconds(6); // Keep pulsePin high for 6 ms

        digitalWrite(pulsePinSnes, LOW); // Write low to the pulsePin
        delayMicroseconds(6); // Keep pulsePin low for 6 ms
    }

    for (int t = 11; t < 16; t++) { // Extra pulses for checking if an SNES controller is connected
        digitalWrite(pulsePinSnes, HIGH); // Write high to the pulsePin
        digitalRead(dataPinSnes) ? snesControllerConnected = false : snesControllerConnected = true; // If read low here a controller is connected
        if (snesControllerConnected) {
            digitalWrite(snesIndicateLed, HIGH);
        }
        else {
            digitalWrite(snesIndicateLed, LOW);
        }
        delayMicroseconds(6); // Keep pulsePin high for 6 ms
        
        digitalWrite(pulsePinSnes, LOW); // Write low to the pulsePin
        delayMicroseconds(6); // Keep pulsePin low for 6 ms
    }

    // If a SNES controller is not connected, and the connection has changed 
    if(!snesControllerConnected && (snesControllerConnected != snesControllerConnectedLast)) { 
        clearAllButtons();
    }
}  

// Handles part of the N64 controller keyboard key pressing
void n64KeyPress(bool check, int button)
{
    
    if(check) { // Check if the button was pressed 
        Keyboard.press(tolower(working_N64_btns[button])); // tolower ensures the lowercase version of the key is pressed
        held_N64_btns[button] = working_N64_btns[button]; // Add the button to the held array
    }
    else { // Or released
        for(int op; op < 18; op++ ) { // Check if the button is currently in the held array
            if (op == button ){ // Do not check for the current key being checked
                continue; 
            }
            if (held_N64_btns[op] == working_N64_btns[button]) { // If the key is in the held array
                held_N64_btns[button] = 0; // Remove it from the held array
                return; // But return so the key does not get released
            }
        }
        // Finally, if the key is not on the held array 
        Keyboard.release(tolower(working_N64_btns[button])); // Release it
        held_N64_btns[button] = 0; // Then remove it from the held array
    }
}

// Built off of the example N64PadToUSB.ino in N64PadForArduino
void n64() {
    static boolean haveController = false;
	if (!haveController) {
		if (pad.begin ()) {
			// Controller detected!
			n64ControllerConnected = true;
			haveController = true;
		}
	} else {
		if (!pad.read ()) {
            // Controller lost
			n64ControllerConnected = false;
			haveController = false;
		} else if (modeSelect == 3) { // Make sure we are in N64 mode
            if (outputMode == 0) {
			    // Begin joystick button pressing
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
			    	if ((pad.buttons & N64Pad::BTN_UP) != 0) 
			    		usbStick.setYAxis (ANALOG_MIN_VALUE);

                    else if ((pad.buttons & N64Pad::BTN_DOWN) != 0) 
			    		usbStick.setYAxis (ANALOG_MAX_VALUE);

                    else 
			    		usbStick.setYAxis (ANALOG_IDLE_VALUE);
			    	
			    	if ((pad.buttons & N64Pad::BTN_LEFT) != 0) 
			    		usbStick.setXAxis (ANALOG_MIN_VALUE);
			    	 
                    else if ((pad.buttons & N64Pad::BTN_RIGHT) != 0) 
			    		usbStick.setXAxis (ANALOG_MAX_VALUE);
			    	 
                    else 
			    		usbStick.setXAxis (ANALOG_IDLE_VALUE);
			    	
			    	// The analog stick gets mapped to the X/Y rotation axes
			    	usbStick.setRxAxis (pad.x);
			    	usbStick.setRyAxis (pad.y);
			    // Send the controller state
			    usbStick.sendState ();
			    
            }
            else {
                // Keyboard pressing
                n64KeyPress((pad.buttons & N64Pad::BTN_A) != 0, 0);
                n64KeyPress((pad.buttons & N64Pad::BTN_B) != 0, 1);
                n64KeyPress((pad.buttons & N64Pad::BTN_Z) != 0, 2);
                n64KeyPress((pad.buttons & N64Pad::BTN_START) != 0, 3);

                // Dpad
                if ((pad.buttons & N64Pad::BTN_UP) == 0) { // Try to stop holding up
                    if (holdCheckUpDown == 1) { // Check if we are holding up
                        n64KeyPress(false, 4); // Release up
                        holdCheckUpDown = 0; // Let us know it's no longer held
                    } 
                }
                if ((pad.buttons & N64Pad::BTN_DOWN) == 0) { // Try to stop holding down
                    if (holdCheckUpDown == 2) { // Check if we are holding down
                        n64KeyPress(false, 5); // Release down
                        holdCheckUpDown = 0; // Let us know it's no longer held
                    } 
                }
                if ((pad.buttons & N64Pad::BTN_UP) != 0) { // Try to press up
                    n64KeyPress(true, 4); // Press up
                    holdCheckUpDown = 1; // We are holding up
                }
                if ((pad.buttons & N64Pad::BTN_DOWN) != 0) { // Try to press down
                    n64KeyPress(true, 5); // Press down
                    holdCheckUpDown = 2; // We are holding down
                }


                if ((pad.buttons & N64Pad::BTN_LEFT) == 0) { // Try to stop holding left
                    if (holdCheckLeftRight == 1) { // Check if we are holding left
                        n64KeyPress(false, 6); // Release left
                        holdCheckLeftRight = 0; // Let us know it's no longer held
                    } 
                }
                if ((pad.buttons & N64Pad::BTN_RIGHT) == 0) { // Try to stop holding right
                    if (holdCheckLeftRight == 2) { // Check if we are holding right
                        n64KeyPress(false, 7); // Release
                        holdCheckLeftRight = 0; // Let us know it's no longer held
                    } 
                }
                if ((pad.buttons & N64Pad::BTN_LEFT) != 0) { // Try to press left
                    n64KeyPress(true, 6); // Press left
                    holdCheckLeftRight = 1; // We are holding left
                }
                if ((pad.buttons & N64Pad::BTN_RIGHT) != 0) { // Try to press right
                    n64KeyPress(true, 7); // Press right
                    holdCheckLeftRight = 2; // We are holding right
                }
                
                n64KeyPress((pad.buttons & N64Pad::BTN_C_DOWN) != 0, 8);
                n64KeyPress((pad.buttons & N64Pad::BTN_C_LEFT) != 0, 9);
                n64KeyPress((pad.buttons & N64Pad::BTN_L) != 0, 10);
                n64KeyPress((pad.buttons & N64Pad::BTN_R) != 0, 11);
                n64KeyPress((pad.buttons & N64Pad::BTN_C_UP) != 0, 12);
                n64KeyPress((pad.buttons & N64Pad::BTN_C_RIGHT) != 0, 13);


                // Joystick
                // The deazone is how much the joytick will need to be moved to detect a press, a higher value means it needs to be moved further and vice versa
                if (pad.y < n64KeyJoystickDeadZone) { // Try to stop holding up if within the deadzone
                    if (holdCheckUpDownN64Joy == 1) { // Check if we are holding up
                        n64KeyPress(false, 14); // Release up
                        holdCheckUpDownN64Joy = 0; // Let us know it's no longer held
                    } 
                }
                if (pad.y > (-1 * n64KeyJoystickDeadZone)) { // Try to stop holding down if within the deadzone
                    if (holdCheckUpDownN64Joy == 2) { // Check if we are holding down
                        n64KeyPress(false, 15); // Release down
                        holdCheckUpDownN64Joy = 0; // Let us know it's no longer held
                    } 
                }
                if (pad.y > n64KeyJoystickDeadZone) { // Try to press up if past the deadzone
                    n64KeyPress(true, 14); // Press up
                    holdCheckUpDownN64Joy = 1; // We are holding up
                }
                if (pad.y < (-1 * n64KeyJoystickDeadZone)) { // Try to press down if past the deadzone
                    n64KeyPress(true, 15); // Press down
                    holdCheckUpDownN64Joy = 2; // We are holding down
                }

                if (pad.x > (-1 * n64KeyJoystickDeadZone)) { // Try to stop holding right if within the deadzone
                    if (holdCheckLeftRightN64Joy == 2) { // Check if we are holding right
                        n64KeyPress(false, 16); // Release right
                        holdCheckLeftRightN64Joy = 0; // Let us know it's no longer held
                    } 
                }

                if (pad.x < n64KeyJoystickDeadZone) { // Try to stop holding left if within the deadzone
                    if (holdCheckLeftRightN64Joy == 1) { // Check if we are holding left
                        n64KeyPress(false, 17); // Release left
                        holdCheckLeftRightN64Joy = 0; // Let us know it's no longer held
                    } 
                }

                if (pad.x > n64KeyJoystickDeadZone) { // Try to press left if past the deadzone
                    n64KeyPress(true, 17); // Press left
                    holdCheckLeftRightN64Joy = 1; // We are holding left
                }

                if (pad.x < (-1 * n64KeyJoystickDeadZone)) { // Try to press right if past the deadzone
                    n64KeyPress(true, 16); // Press right
                    holdCheckLeftRightN64Joy = 2; // We are holding right
                }  
            }
		}
    } 
}

// Check what postion the rotary dial is in and sets the mode accordingly
void checkDial() {
  
    if (!digitalRead(serialDial)) {
        modeSelect = 0;
    }                                                        
    else if (!digitalRead(nesDial)) {
        modeSelect = 1;
    }
    else if (!digitalRead(snesDial)) {
        modeSelect = 2;
    }
    else if (!digitalRead(n64Dial)) {
        modeSelect = 3;
    }
    else { // Error/default state 
        modeSelect = 0;
    }
}

// Sets the controller lights on or off depending on if the controller is connected
void updateLights() {
    //digitalWrite(nesIndicateLed, nesControllerConnected );
    //digitalWrite(snesIndicateLed, snesControllerConnected );
    digitalWrite(n64IndicateLed, n64ControllerConnected );
}

void setup() {

    Serial.begin(9600); // Start serial

    // Pin mode setup
    pinMode(pulsePinNes, OUTPUT);
    pinMode(latchPinNes, OUTPUT);
    pinMode(dataPinNes, INPUT);

    pinMode(pulsePinSnes, OUTPUT);
    pinMode(latchPinSnes, OUTPUT);
    pinMode(dataPinSnes, INPUT);

    pinMode(serialDial, INPUT_PULLUP);
    pinMode(nesDial, INPUT_PULLUP); 
    pinMode(snesDial, INPUT_PULLUP);
    pinMode(n64Dial, INPUT_PULLUP);
    pinMode(outModeSwitch, INPUT_PULLUP);

    pinMode(nesIndicateLed, OUTPUT);
    pinMode(snesIndicateLed, OUTPUT);
    pinMode(n64IndicateLed, OUTPUT);

    usbStick.begin (false);		// Apparently minimizes lag
	usbStick.setXAxisRange (ANALOG_MIN_VALUE, ANALOG_MAX_VALUE);
	usbStick.setYAxisRange (ANALOG_MIN_VALUE, ANALOG_MAX_VALUE);
	usbStick.setRxAxisRange (ANALOG_MIN_VALUE, ANALOG_MAX_VALUE);
	usbStick.setRyAxisRange (ANALOG_MAX_VALUE, ANALOG_MIN_VALUE);

    loadKeyboardArrays(); // Load the keyboard arrays
}

void loop() {  
  
    // Check the output mode switch
    outputMode = digitalRead(outModeSwitch);
    if(outputMode != outputModelast) {
        clearAllButtons(); // If the output mode switch changed state, clear all buttons
    }
    outputModelast = outputMode;

    // Check the controller selection dial state
    checkDial();
    if( modeSelectLast != modeSelect) {
        clearAllButtons(); // If the mode dial changed state, clear all buttons
    }
    modeSelectLast = modeSelect;


    // Main stuff
    if (modeSelect == 0) {
        serialActions(); // Only call serial actions if in serial mode
        if (mappingChanged) { // Check if any of the mappings where changed
            loadKeyboardArrays(); // If they were, reload the arrays
            mappingChanged = 0;
        }
    }
    else {
        nes();
        snes();
        n64();
        updateLights(); // Update all the lights
    }         
    
}