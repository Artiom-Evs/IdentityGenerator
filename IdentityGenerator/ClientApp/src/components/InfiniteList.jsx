import React, { useState } from "react";
import InfiniteScroll from "react-infinite-scroll-component";

export const InfiniteList = ({ data, getMore, onItemsCountChanged }) => {
    const [items, setItems] = useState(data);
    
    const getMoreItems = async () => {
        const newItems = await getMore();
        onItemsCountChanged(items.length + newItems.length)
        setItems((items) => [...items, ...newItems]);
    };

    return (
        <InfiniteScroll
            height={450}
            dataLength={items.length}
            next={getMoreItems}
            hasMore={true}
            loader={<h3> Loading...</h3>}
        >
            <table className="table table-stripped">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Name</th>
                        <th>Address</th>
                        <th>Phone</th>
                    </tr>
                </thead>
                <tbody>
                    {items.map((data, i) => (
                        <tr key={i}>
                            <td>{i + 1}</td>
                            <td>{data.name}</td>
                            <td>{data.address}</td>
                            <td>{data.phone}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </InfiniteScroll>
    );
};
