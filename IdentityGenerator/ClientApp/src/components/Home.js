import React, { Component } from 'react';
import { ToolbarPanel } from './ToolbarPanel';

export class Home extends Component {
    static displayName = Home.name;
    regions = ["BY"];

    constructor(props) {
        super(props);
        this.state = {
            items: [],
            region: this.regions[0],
            errorsCount: 0,
            itemsCount: 10,
            seedNumber: 0,
            loading: true
        };

        this.onButtonClick = this.onButtonClick.bind(this);
    }

    componentDidMount() {
        this.populateFakeData();
    }

    onButtonClick = (e) => {
        this.setState({ loading: true, ...e });
        this.populateFakeData();
    }

    renderToolbar() {
        return (
            <ToolbarPanel
                regions={this.regions}
                onButtonClick={(e) => this.onButtonClick(e)}
            />
        );
    }

    static renderFakeDataTable(items) {
        return (
            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Name</th>
                        <th>Address</th>
                        <th>Phone</th>
                    </tr>
                </thead>
                <tbody>
                    {items.map((item, i) =>
                        <tr key={i}>
                            <td>{i + 1}</td>
                            <td>{item.name}</td>
                            <td>{item.address}</td>
                            <td>{item.phone}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderFakeDataTable(this.state.items);

        return (
            <div>
                <h1>Fake data</h1>
                {this.renderToolbar()}
                {contents}
            </div>
        );
    }

    async populateFakeData() {
        const response = await fetch('/api/fakedata');
        const data = await response.json();
        this.setState({ items: data, loading: false });
    }
}
