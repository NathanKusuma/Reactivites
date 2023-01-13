import { observer } from "mobx-react-lite";
import React from "react";
import { Grid } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import ActivityDetails from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";
import ActivityList from "./ActivityList";

export default observer(function ActivityDashboard() {
  const { activityStore } = useStore();
  const { selectedActivity, editMode } = activityStore;

  return (
    <Grid>
      <Grid.Column width="10">
        <ActivityList />
      </Grid.Column>
      <Grid.Column width="6">
        {selectedActivity && !editMode && <ActivityDetails />}

        {editMode && <ActivityForm />}
      </Grid.Column>
    </Grid>
  );
});
//Full grid pada semantic UI berjumlah 16, beda dengan bootstrap yang berjumlah 12
//Mengambil array ke 0 pada interface activites hanya untuk dapat terconnect dengan interface pada models activity
//activity pada activity details terbaca null sehingga perlu di deklarasikan code seperti ini
// {selectedActivity && <ActivityDetails activity={selectedActivity} />}
// Code diatas berarti apabila activities tidak null atau undefined maka kode di samping kanan && akan di eksekusi
