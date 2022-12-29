LinkedList<int> activePins = LinkedList<int>();
LinkedList<int> pinTypes = LinkedList<int>();
LinkedList<int> pinModes = LinkedList<int>();
LinkedList<double> States = LinkedList<double>();
LinkedList<String> TempStrings = LinkedList<String>();


String payload;
bool GotPins = false;


void SetupStateManager() {
  if (client.connect(server, serverPort))
    for (int i = 0; i < 5; i++) {
      Serial.println("connected");
      // transmit data
      //signup arduino with own IP;
      client.println("SetIP:" + WiFi.localIP().toString() + ";" + "¶" + "SetID:" + id + ";" + "¶");
      delay(250);
      GetPins();
    }
  else {
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
  client.println("GetPins:" + (String)id + ";" + "¶");
  String message = "";
  while (!GotPins)
    if (client.available()) {
      char c = client.read();
      if (c != '¶') {
        if (c != ';') {
          message = +c;
          Serial.print(c);
        }
        else{
          TempStrings.add(message);
          message = "";
        }
      } else {
        if (TempStrings.get(0) == "SendPinsTo:" + (String)id) {
          TempStrings.remove(0);
          for(int i=0; i < TempStrings.size(); i++){
            int from = TempStrings.get(i).indexOf(':');
            int to = TempStrings.get(i).length();
            String subString = TempStrings.get(i).substring(from, to);
            Serial.print(subString);
          }
        } else
          TempStrings.clear();
        Serial.print("Wrong message received");
      }
    }
}

void CheckStates() {
  if (client.available()) {
    char c = client.read();
    Serial.print(c);
  }

  if (!client.connected()) {
    Serial.println();
    Serial.println("disconnecting.");
    client.stop();
    delay(100000);
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
