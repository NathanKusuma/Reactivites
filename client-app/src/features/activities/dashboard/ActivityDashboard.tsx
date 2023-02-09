import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { Grid } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useStore } from "../../../app/stores/store";
import ActivityFilters from "./ActivityFilters";
import ActivityList from "./ActivityList";

export default observer(function ActivityDashboard() {
  const { activityStore } = useStore();
  const { loadActivities, activityRegistry } = activityStore;

  useEffect(() => {
    if (activityRegistry.size <= 1) {
      loadActivities();
    }
  }, [activityRegistry.size, loadActivities]);

  if (activityStore.loadingInitial)
    return <LoadingComponent content="Loading Activities...." />;

  return (
    <Grid>
      <Grid.Column width="10">
        <ActivityList />
      </Grid.Column>
      <Grid.Column width="6">
        {/* {selectedActivity && !editMode && <ActivityDetails />}

        {editMode && <ActivityForm />} */}
        <ActivityFilters />
      </Grid.Column>
    </Grid>
  );
});
//Full grid pada semantic UI berjumlah 16, beda dengan bootstrap yang berjumlah 12
//Mengambil array ke 0 pada interface activites hanya untuk dapat terconnect dengan interface pada models activity
//activity pada activity details terbaca null sehingga perlu di deklarasikan code seperti ini
// {selectedActivity && <ActivityDetails activity={selectedActivity} />}
// Code diatas berarti apabila activities tidak null atau undefined maka kode di samping kanan && akan di eksekusi
