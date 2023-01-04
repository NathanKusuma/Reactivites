/* eslint-disable jsx-a11y/anchor-is-valid */
import React from "react";
import { Button, Card, Image } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";

interface Props {
  activity: Activity; //tidak diberi kurung kotak karena hanya mengambil satu activity dari models activity.ts
  cancelSelectActivity: () => void; //Karena ini function harus diberi return. diberi return void karena aku tidak ingin mereturn apa"
  openForm: (id: string) => void;
}

export default function ActivityDetails({
  activity,
  cancelSelectActivity,
  openForm,
}: Props) {
  return (
    <div>
      <Card fluid>
        <Image src={`/assets/categoryImages/${activity.category}.jpg`} />
        <Card.Content>
          <Card.Header>{activity.title}</Card.Header>
          <Card.Meta>
            <span>{activity.date}</span>
          </Card.Meta>
          <Card.Description>{activity.description}</Card.Description>
        </Card.Content>
        <Card.Content extra>
          <Button.Group widths="2">
            <Button
              onClick={() => openForm(activity.id)}
              basic
              color="blue"
              content="Edit"
            />
            <Button
              onClick={cancelSelectActivity}
              basic
              color="grey"
              content="Cancel"
            />
          </Button.Group>
        </Card.Content>
      </Card>
    </div>
  );
}
//Pada button onClick diberi tidak diberi arrow function karena pada cancelSelectActivity tidak memiliki params sehingga akan di eksuski apabila button itu di click
