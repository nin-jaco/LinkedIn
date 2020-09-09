import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Welcome to the .NEt Core React Exercise</h1>
        <p>Use this manager to manage your trips</p>
        <ul>
          <li><a href='#'>Add a new trip</a></li>
          <li><a href='#'>Update a Trip</a></li>
          <li><a href='#'>Delete a Trip</a></li>
          <li><a href='#'>View all Trips</a></li>
        </ul>        
      </div>
    );
  }
}
