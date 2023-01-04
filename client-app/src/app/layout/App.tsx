import { useEffect, useState } from "react";
import axios from "axios";
import { Container } from "semantic-ui-react";
import { Activity } from "../models/activity";
import NavBar from "./NavBar";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import { v4 as uuid } from "uuid";
function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  //Notes
  //activities untuk menyimpan data, setActivities untuk mengatur data
  //diberi kurung pada useState karena strict pada typescript,sehingga activites dapat dipanggil pada return react
  const [selectedActivity, setSelectedActivity] = useState<
    Activity | undefined
  >(undefined);

  const [editMode, setEditMode] = useState(false);

  useEffect(() => {
    axios
      .get<Activity[]>("http://localhost:5000/api/activities")
      .then((response) => {
        setActivities(response.data);
      });
  }, []);
  //Note
  //Axios berfungsi untuk get API dari back-end
  //<Activity[]> Mengambil Models pada array Activity sebagai interface
  //diberi kurung kosong di akhir useEffect agar tidak looping berulang

  function handleSelectActivity(id: string) {
    setSelectedActivity(activities.find((x) => x.id === id)); //Membuat callback function untuk get ID
  } //Berfungsi untuk get activity yang akan ditampilkan

  function handleCancelSelectActivity() {
    setSelectedActivity(undefined);
  }

  function handleFormOpen(id?: string) {
    id ? handleSelectActivity(id) : handleCancelSelectActivity(); //Menggunakan ternary operation
    setEditMode(true);
  }
  function handleFormClose() {
    setEditMode(false);
  }

  function handleCreateOrEditActivity(activity: Activity) {
    activity.id
      ? setActivities([
          ...activities.filter((x) => x.id !== activity.id),
          activity,
        ]) //Membuat callback function dengan params x untuk membuat pertidaksamaan
      : setActivities([...activities, { ...activity, id: uuid() }]); //Menggunakan uuid untuk mengatur Guid(random) ID
    setEditMode(false);
    setSelectedActivity(activity);
  }

  function handleDeleteActivity(id: string) {
    setActivities([...activities.filter((x) => x.id !== id)]);
  }

  return (
    <>
      <NavBar openForm={handleFormOpen} />
      <Container style={{ marginTop: "7em" }}>
        <ActivityDashboard
          activities={activities}
          selectedActivity={selectedActivity}
          selectActivity={handleSelectActivity}
          cancelSelectActivity={handleCancelSelectActivity}
          editMode={editMode}
          openForm={handleFormOpen}
          closeForm={handleFormClose}
          createOrEdit={handleCreateOrEditActivity}
          deleteActivity={handleDeleteActivity}
        />
      </Container>
    </>
  );
}

export default App;
