LinkedList<int> activePins = LinkedList<int>();
LinkedList<int> pinTypes = LinkedList<int>();
LinkedList<int> pinModes = LinkedList<int>();
LinkedList<double> States = LinkedList<double>();
LinkedList<String> TempStrings = LinkedList<String>();


String payload;
bool GotPins = false;


void SetupStateManager() {
  if (client.connect(server, serverPort)) {
    Serial.println("connected");
    // transmit data
    //signup arduino with own IP;
    client.println("SetID:" + (String)id + ";" + "SetIP:" + WiFi.localIP().toString() + ";" + "$");
    delay(250);
    GetPins();
  } else {
    Serial.println("connection failed");
  }



  //   for (int i = 0; i < array.size(); i++) {
  //     if (array[i]["pinMode"].as<int>() == 0) {
  //       pinMode(array[i]["pinNameString"].as<int>(), INPUT) ;
  //     }
  //     else {
  //       pinMode(array[i]["pinNameString"].as<int>(), OUTPUT) ;
  //     }
  //     activePins.add(array[i]["pinNameString"].as<int>());
  //     pinModes.add(array[i]["pinMode"].as<int>());
  //     pinTypes.add(array[i]["pinType"].as<int>());
  //     States.add(0);
  //   }
  //   HasSetup = true;
}
// else {
//   Serial.print("Error code: ");
//   Serial.println(httpResponseCode);
//   }


// }

void GetPins() {
  client.println("GetPins:" + (String)id + ";" + "$");
  String message = "";
  while (!GotPins)
    if (client.available()) {
      char c = client.read();
      if (c != '$') {
        if (c != ';') {
          message += c;
          //Serial.print(c);
        } else {
          TempStrings.add(message);
          message = "";
        }
      } else {
        for (int i = 0; i < TempStrings.size(); i++) {
          Serial.println(TempStrings.get(i));
          int from = TempStrings.get(i).indexOf(':');
          int to = TempStrings.get(i).length();
          String subString = TempStrings.get(i).substring(from + 1, to);
          if (subString[0] == 'A') {
            pinTypes.add(0);
          } else if (subString[0] == 'D') {
            pinTypes.add(1);
          } else if (subString[0] == 'V') {
            pinTypes.add(2);
          }
          subString.remove(0, 1);

          if (subString[subString.length() - 1] == 'I') {
            subString.remove(subString.length() - 1);
            pinMode(subString.toInt(), INPUT);
          } else if (subString[subString.length() - 1] == 'O') {
            subString.remove(subString.length() - 1);
            pinMode(subString.toInt(), OUTPUT);
          } else if (subString[subString.length() - 1] == 'N') {
          }
        }
        GotPins = true;
        Serial.println("Pins Setup");
      }
    }
  TempStrings.clear();
}

String message = "";

void(* resetFunc) (void) = 0; //declare reset function @ address 0
 
void CheckStates() {
  if (!client.connected()) {
    Serial.println();
    Serial.println("disconnecting.");
    client.stop();
    //resetFunc();  //call reset
    delay(100000);
  }
  if (client.available()) {
    char c = client.read();
    if (c != '$') {
      if (c != ';') {
        message += c;
      } else {
        TempStrings.add(message);
        message = "";
      }
    } else {
      if (TempStrings.size() > 0) {
        for (int i = 0; i < TempStrings.size(); i++) {
          if(TempStrings.get(i).indexOf('|')){
            int fromcmd = TempStrings.get(i).indexOf(':');
            int fromsep = TempStrings.get(i).indexOf('|');
            int to = TempStrings.get(i).length();
            String subStringValue = TempStrings.get(i).substring(fromcmd + 1, fromsep);
            String subStringValue2 = TempStrings.get(i).substring(fromsep + 1, to);
            String subString = TempStrings.get(i).substring(0, fromcmd);
            Serial.println(subString);
            Serial.println(subStringValue);
            Serial.println(subStringValue2);
            if(subString == "SendPin"){
              if(subStringValue2 == "1"){
                digitalWrite(subStringValue.toInt(), HIGH);
              }
              else if (subStringValue2 == "0")   {
                digitalWrite(subStringValue.toInt(), LOW);
              }           
            }
          }
          else{
            int from = TempStrings.get(i).indexOf(':');
            int to = TempStrings.get(i).length();
            String subStringValue = TempStrings.get(i).substring(from + 1, to);
            String subString = TempStrings.get(i).substring(0, from);
            Serial.println(subString);
            Serial.println(subStringValue);
          }
        }
      }
      TempStrings.clear();
    }
  }



  // HTTPClient PostClient;

  // for (int i = 0; i < activePins.size(); i++) {
  //   if (pinTypes.get(i) == 1) {
  //     if (pinModes.get(i) == 0) {
  //       double pinVal = digitalRead(activePins.get(i));
  //       if (pinVal != States.get(i)) {
  //         String URL = serverPostPath + activePins.get(i) + "/" + pinVal + "/" + Id;
  //         Serial.println(URL);
  //         States.set(i, pinVal);
  //         PostClient.begin(client, URL);
  //         PostClient.POST({});
  //         Serial.print("Pin: ");
  //         Serial.print(activePins.get(i));
  //         Serial.print(" State: ");
  //         Serial.println(States.get(i));
  //       }
  //     }
  //   }
  //   else if (pinTypes.get(i) == 0) {
  //     if (pinModes.get(i) == 0) {
  //       double pinVal = analogRead(activePins.get(i));
  //       if (pinVal != States.get(i)) {
  //         String URL = serverPostPath + activePins.get(i) + "/" + pinVal+ "/" + Id;
  //         Serial.println(URL);
  //         States.set(i, pinVal);
  //         PostClient.begin(client, URL);
  //         PostClient.POST({});
  //         Serial.print("Pin: ");
  //         Serial.print(activePins.get(i));
  //         Serial.print(" State: ");
  //         Serial.println(States.get(i));
  //       }
  //     }
  //   }
  // }
  // PostClient.end();
}
