import React, { Component } from 'react';
import { ToolbarPanel } from './ToolbarPanel';
import { InfiniteList } from './InfiniteList';

export class Home extends Component {
    static displayName = Home.name;
    regions = ["BY"];

    constructor(props) {
        super(props);
        this.state = {
            items: [],
            itemsLoaded: 0,
            region: this.regions[0],
            errorsCount: 0,
            itemsCount: 10,
            seedNumber: 0,
            loading: true
        };

        this.getFakeData = this.getFakeData.bind(this);
        this.onItemsCountChanged = this.onItemsCountChanged.bind(this);
        this.onButtonClick = this.onButtonClick.bind(this);
        this.onDownloadClick = this.onDownloadClick.bind(this);
    }

    componentDidMount() {
        this.populateFakeData();
    }

    buildUrl() {
        const { region, itemsLoaded, itemsCount, errorsCount, seedNumber } = this.state;
        let url = "/api/fakedata";
        url += `?region=${region}&startItem=${itemsLoaded}&itemsCount=${itemsCount}&errorsCount=${errorsCount}&seedNumber=${seedNumber}`;
        return url;
    }

    buildDownloadUrl() {
        const { region, itemsLoaded, errorsCount, seedNumber } = this.state;
        let url = "/api/fakedata/csv";
        url += `?region=${region}&itemsCount=${itemsLoaded}&errorsCount=${errorsCount}&seedNumber=${seedNumber}`;
        return url;
    }

    getFakeData = async () => {
        const url = this.buildUrl();
        const response = await fetch(url);
        const data = await response.json();
        return data;
    }

    onItemsCountChanged = (e) => {
        this.setState({ itemsLoaded: e });
    }

    onButtonClick = (e) => {
        this.setState({ items: [], itemsLoaded: 0, loading: true, ...e }, this.populateFakeData);
    }

    onDownloadClick = (e) => {
        const link = document.createElement('a');
        link.href = this.buildDownloadUrl();
        document.body.appendChild(link);
        link.click();
        link.parentNode.removeChild(link);
    }

    renderToolbar() {
        return (
            <ToolbarPanel
                regions={this.regions}
                onButtonClick={(e) => this.onButtonClick(e)}
                onDownloadClick={this.onDownloadClick}
            />
        );
    }

    renderFakeData() {
        return (
            <InfiniteList
                data={this.state.items}
                getMore={this.getFakeData}
                onItemsCountChanged={this.onItemsCountChanged}
            />
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderFakeData();

        return (
            <div>
                <h1>Fake data</h1>
                {this.renderToolbar()}
                {contents}
            </div>
        );
    }

    async populateFakeData() {
        const data = await this.getFakeData();
        this.setState({ items: data, itemsLoaded: data.length, loading: false });
    }
}
