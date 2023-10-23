import { Component } from "react";
import { Button, FormControl, Grid, Input, InputLabel, MenuItem, Select, Slider, TextField, Typography } from "@mui/material";

export class ToolbarPanel extends Component {
    static displayName = ToolbarPanel.name;

    constructor(props) {
        super(props);

        this.onCountryChange = this.onCountryChange.bind(this);
        this.onSliderChange = this.onSliderChange.bind(this);
        this.onSliderInputChange = this.onSliderInputChange.bind(this);
        this.onRandomClick = this.onRandomClick.bind(this);

        this.regions = props.regions;
        this.onButtonClick = props.onButtonClick;
        this.onDownloadClick = props.onDownloadClick;

        this.state = {
            region: this.regions[0],
            errorsCount: 0,
            seedNumber: 0
        };
    }

    onCountryChange = (e) => {
        this.setState({ country: e.target.value });
    }

    onSliderChange = (e) => {
        this.setState({ errorsCount: e.target.value * 10 });
    }

    onSliderInputChange = (e) => {
        var value = parseInt(e.target.value);
        
        if (e.target.value === "") {
            value = 0;
        }
        if (isNaN(value)) {
            value = this.state.seedNumber;
        }
        else if (value < 0) {
            value = 0;
        }
        else if (value > 1000) {
            value = 1000;
        }

        e.target.value  = value;
        this.setState({ errorsCount: value });
    }

    onSeedChanged = (e) => {
        var value = parseInt(e.target.value);

        if (e.target.value === "") {
            value = 0;
        }
        if (isNaN(value)) {
            e.target.value = this.state.seedNumber;
            return;
        }

        e.target.value = value;
        this.setState({ seedNumber: value });
    }

    onRandomClick = () => {
        this.onButtonClick(this.state);
    }

    render() {
        const { region, errorsCount, seedNumber } = this.state;

        return (
            <Grid
                container
                spacing
                direction="row"
                justifyContent="flex-start"
                alignItems="center"
            >
                <Grid item >
                    <FormControl variant="standard" sx={{ minWidth: 120 }}>
                        <Typography id="label1">Region</Typography >
                        <Select
                            labelId="label1"
                            min={0}
                            max={100}
                            value={region}
                            onChange={this.onCountryChange}
                        >
                            {this.regions.map((r, i) => 
                                <MenuItem key={i} value={r}>{r}</MenuItem>
                            )}
                        </Select>
                    </FormControl>
                </Grid>
                
                <Grid item minWidth={240}>
                    <Typography id="input-slider">Errors count</Typography>
                    <Grid container spacing alignItems="center">
                        <Grid item xs={8}>
                            <Slider 
                                value={ ~~(errorsCount / 10) }
                                onChange={this.onSliderChange}
                                aria-labelledby="input-slider"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <TextField 
                                type="number"
                                variant="standard"
                                value={errorsCount}
                                onChange={this.onSliderInputChange}
                                step={10}
                                min={0}
                                max={1000}
                                aria-labelledby="input-slider"
                            />
                        </Grid>
                    </Grid>
                </Grid>

                <Grid item>
                    <InputLabel id="input-seed-label">Seed number</InputLabel >
                    <TextField 
                        labelId="input-seed-label" 
                        variant="standard"
                        type="number"
                        value={seedNumber}
                        onChange={this.onSeedChanged}
                    />
                </Grid>

                <Grid item Item>
                    <Button  
                        variant="outlined"
                        onClick={this.onRandomClick}
                    >Random</Button>
                </Grid>

                <Grid item Item>
                    <Button
                        variant="outlined"
                        onClick={this.onDownloadClick}
                    >Download as CSV</Button>
                </Grid>
            </Grid>
        )
    }
}