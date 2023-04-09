import {
  IonButton,
  IonCard,
  IonCardContent,
  IonCol,
  IonContent,
  IonGrid,
  IonIcon,
  IonItem,
  IonPage,
  IonRow,
  IonSpinner,
  IonText,
} from "@ionic/react";
import { useEffect, useState } from "react";
import { Geolocation } from "@capacitor/geolocation";
import { Device } from "@capacitor/device";
import { environment } from "../environment";
import { cloudDownloadOutline } from "ionicons/icons";

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
  const [devideId, setDeviceId] = useState<string | undefined>();
  const [currentTime, setCurrentTime] = useState<string | undefined>();

  useEffect(() => {
    const interval = setInterval(async () => {
      try {
        const { latitude, longitude } = await currentPosition();
        setLatitude(latitude);
        setLongitude(longitude);
        const id = await getDeviceId();
        setDeviceId(id);
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

  useEffect(() => {
    const date = new Date().toLocaleString();
    setCurrentTime(date);
    setInterval(() => {
      const date = new Date().toLocaleString();
      setCurrentTime(date);
    }, 1000);
  }, []);

  return (
    <IonPage>
      <IonContent>
        <IonCard color={"tertiary"}>
          <IonCardContent color="dark">
            <IonGrid>
              <IonCol>
                <IonRow>
                  <IonText>
                    <h3 style={{ fontWeight: "600" }}>ID</h3>
                  </IonText>
                </IonRow>
                <IonRow>
                  <IonText>
                    <h2 style={{ fontWeight: "bold" }}>
                      {devideId ? (
                        devideId
                      ) : (
                        <IonItem>
                          <IonSpinner name="crescent" />
                        </IonItem>
                      )}
                    </h2>
                  </IonText>
                </IonRow>
              </IonCol>
              <IonCol>
                <IonRow>
                  <IonText>
                    <h3 style={{ fontWeight: "600" }}>LATITUDE</h3>
                  </IonText>
                </IonRow>
                <IonRow>
                  <IonText>
                    <h2 style={{ fontWeight: "bold" }}>
                      {latitude ? (
                        latitude
                      ) : (
                        <IonItem>
                          <IonSpinner name="crescent" />
                        </IonItem>
                      )}
                    </h2>
                  </IonText>
                </IonRow>
              </IonCol>
              <IonCol>
                <IonRow>
                  <IonText>
                    <h3 style={{ fontWeight: "600" }}>LONGITUDE</h3>
                  </IonText>
                </IonRow>
                <IonRow>
                  <IonText>
                    <h2 style={{ fontWeight: "bold" }}>
                      {longitude ? (
                        longitude
                      ) : (
                        <IonItem>
                          <IonSpinner name="crescent" />
                        </IonItem>
                      )}
                    </h2>
                  </IonText>
                </IonRow>
              </IonCol>
              <IonCol>
                <IonRow>
                  <IonText>
                    <h3 style={{ fontWeight: "600" }}>DATA & TEMPO</h3>
                  </IonText>
                </IonRow>
                <IonRow>
                  <IonText>
                    <h2 style={{ fontWeight: "bold" }}>
                      {currentTime ? (
                        currentTime
                      ) : (
                        <IonItem color={"danger"}>
                          <IonSpinner name="crescent" />
                        </IonItem>
                      )}
                    </h2>
                  </IonText>
                </IonRow>
              </IonCol>
            </IonGrid>
            <IonButton
              onClick={(e) => {
                e.preventDefault();
                window.location.replace(
                  `https://localhost:7072/api/Locations/file/${devideId}/download`
                );
              }}
              expand="full"
              color={"tertiary"}
              disabled={devideId ? false : true}
            >
              <span
                style={{
                  textDecoration: "none",
                  fontWeight: "400",
                  color: "white",
                }}
              >
                Download
              </span>
              {devideId ? (
                <IonIcon
                  slot="end"
                  icon={cloudDownloadOutline}
                  color="dark"
                ></IonIcon>
              ) : (
                <IonItem>
                  <IonSpinner name="crescent" />
                </IonItem>
              )}
            </IonButton>
          </IonCardContent>
        </IonCard>
      </IonContent>
    </IonPage>
  );
};

export default Tab1;
