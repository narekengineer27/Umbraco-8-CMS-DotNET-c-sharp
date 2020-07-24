import React from 'react';
import { Accordion, Icon } from 'semantic-ui-react'
import { IOutcome } from '../lib/SportsBookModel/interfaces/IOutcome';

interface IOutcomePropTypes {
  outcome: IOutcome;
}

class Outcome extends React.Component<IOutcomePropTypes> {

  render() {
    return (
      <div>{this.props.outcome.name}</div>
    );
  }
}

export default Outcome;
