import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { Activity } from "../models/activity";
import { v4 as uuid } from "uuid";

export default class activityStore {
  // activities: Activity[] = [];
  activityRegistry = new Map<string, Activity>();
  selectedActivity: Activity | undefined = undefined;
  editMode = false;
  loading = false;
  loadingInitial = false;

  constructor() {
    makeAutoObservable(this);
  }

  get activitiesByDate() {
    return Array.from(this.activityRegistry.values()).sort(
      (a, b) => Date.parse(a.date) - Date.parse(b.date)
    );
  } //Computed property to sort by date

  loadActivities = async () => {
    this.setLoadingInitial(true);
    try {
      const activities = await agent.Activities.list();
      activities.forEach((activity) => {
        this.setActivity(activity);
      });
      this.setLoadingInitial(false);
    } catch (error) {
      console.log(error);
      this.setLoadingInitial(false);
    }
  };
  //Note
  //Pada baris sebelum try catch dilakukan Secara synchronus,pada try and catch dilakukan secara asynchronus
  //Menggunaka arrow function untuk mengaitkan dengan class activityStore

  loadActivity = async (id: string) => {
    let activity = this.getActivity(id);
    if (activity) {
      this.selectedActivity = activity;
      return activity;
    } else {
      this.setLoadingInitial(true);
      try {
        activity = await agent.Activities.details(id);
        this.setActivity(activity);
        runInAction(() => {
          this.selectedActivity = activity;
        });
        this.setLoadingInitial(false);
        return activity;
      } catch (error) {
        console.log(error);
        this.setLoadingInitial(false);
      }
    }
  };

  private getActivity = (id: string) => {
    return this.activityRegistry.get(id);
  };

  private setActivity = (activity: Activity) => {
    activity.date = activity.date.split("T")[0];
    this.activityRegistry.set(activity.id, activity); //using map
    // this.activities.push(activity); using array
  };
  //Note
  //Pada try colomn digunakan untuk merubah date menjadi type date bukan string
  //T merupakan parameter dari type axios pada file agent.ts

  setLoadingInitial = (state: boolean) => {
    this.loadingInitial = state;
  }; //setLoadingInitial merupakan new action untuk mencegah MobX strict,karena jika langsung mengidentifikasi di dalam action loadActivities perlu di tambahkan runInAction

  createActivity = async (activity: Activity) => {
    this.loading = true;
    activity.id = uuid();

    try {
      await agent.Activities.create(activity);
      runInAction(() => {
        // this.activities.push(activity);using array
        this.activityRegistry.set(activity.id, activity); //using map
        this.selectedActivity = activity;
        this.editMode = false;
        this.loading = false;
      });
    } catch (error) {
      console.log(error);
      runInAction(() => {
        this.loading = false;
      });
    }
  };

  updateActivity = async (activity: Activity) => {
    this.loading = true;
    try {
      await agent.Activities.update(activity);
      runInAction(() => {
        // this.activities = [
        //   ...this.activities.filter((a) => a.id !== activity.id),
        //   activity,
        // ]; using array
        this.activityRegistry.set(activity.id, activity); //using map
        this.selectedActivity = activity;
        this.editMode = false;
        this.loading = false;
      });
    } catch (error) {
      console.log(error);
      runInAction(() => {
        this.loading = false;
      });
    }
  };
  deleteActivity = async (id: string) => {
    this.loading = true;
    try {
      await agent.Activities.delete(id);
      runInAction(() => {
        // this.activities = [...this.activities.filter((a) => a.id !== id)];//using array
        this.activityRegistry.delete(id); //using map
        this.loading = false;
      });
    } catch (error) {
      console.log(error);
      runInAction(() => {
        this.loading = false;
      });
    }
  };
}

// selectActivity = (id: string) => {
//   this.selectedActivity = this.activityRegistry.get(id); //using map
//   // this.selectedActivity = this.activities.find((a) => a.id === id); using array
// }; //Untuk select data

// cancelSelectedActivity = () => {
//   this.selectedActivity = undefined;
// }; //Untuk cancel select data

// openForm = (id?: string) => {
//   id ? this.selectActivity(id) : this.cancelSelectedActivity();
//   this.editMode = true;
// };

// closeForm = () => {
//   this.editMode = false;
// };
