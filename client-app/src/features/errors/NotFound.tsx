import React from "react";
import { Link } from "react-router-dom";
import { Header, Icon, Segment, Button } from "semantic-ui-react";

export default function NotFound() {
  return (
    <Segment placeholder>
      <Header icon>
        <Icon name="search" />
        Oops - we've looking for what you search but could not found what your
        looking for!
      </Header>
      <Segment.Inline>
        <Button as={Link} to="/activities" content="Return To Activites Page" />
      </Segment.Inline>
    </Segment>
  );
}
