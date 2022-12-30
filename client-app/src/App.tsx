import React, { useEffect, useState } from "react";
import "./App.css";
import axios from "axios";
import { Header, List } from "semantic-ui-react";
import Button from "semantic-ui-react/dist/commonjs/elements/Button";

function App() {
  const [activities, setActivities] = useState([]);
  //activities untuk menyimpan data, setActivities untuk mengatur data
  //diberi kurung pada useState karena strict pada typescript,sehingga activites dapat dipanggil pada return react

  useEffect(() => {
    axios.get("http://localhost:5000/api/activities").then((response) => {
      setActivities(response.data);
    });
  }, []); //diberi kurung kosong agar tidak looping berulang

  return (
    <div>
      <Header as="h2" icon="users" content="Reactivities" />
      <List>
        {activities.map(
          (
            activity: any //Diberi any karena parameter tidak memiliki interface
          ) => (
            <List.Item key={activity.id}>
              {/* Diberi id sebagai penanda list mana yang akan diambil */}
              {activity.title}
            </List.Item>
          )
        )}
      </List>
      <Button content="test" />
    </div>
  );
}

export default App;
