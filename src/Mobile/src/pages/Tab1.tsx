import { IonContent, IonPage } from "@ionic/react";
import { useEffect, useState } from "react";
import { Geolocation } from "@capacitor/geolocation";
import { Device } from "@capacitor/device";
import { environment } from "../environment";

const currentPosition = async () => {
  const { coords } = await Geolocation.getCurrentPosition();
  const { latitude, longitude } = coords;
  return { latitude, longitude };
};

const getDeviceId = async () => {
  const { uuid } = await Device.getId();
  return uuid;
};

const Tab1: React.FC = () => {
  const [latitude, setLatitude] = useState<number | undefined>();
  const [longitude, setLongitude] = useState<number | undefined>();

  useEffect(() => {
    const interval = setInterval(async () => {
      try {
        const { latitude, longitude } = await currentPosition();
        setLatitude(latitude);
        setLongitude(longitude);
        const id = await getDeviceId();
        fetch(`${environment.API_URL}/api/locations/save`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            latitude: latitude.toString(),
            longitude: longitude.toString(),
            deviceId: id.toString(),
          }),
        });
      } catch (error: any) {
        console.log(error);
      }
    }, 5000);
    return () => clearInterval(interval);
  }, []);

  return (
    <IonPage>
      <IonContent fullscreen color={"light"}>
        {latitude} {longitude}
      </IonContent>
    </IonPage>
  );
};

export default Tab1;
