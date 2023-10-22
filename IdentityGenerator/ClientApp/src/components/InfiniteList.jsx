import React, { useState } from "react";
import InfiniteScroll from "react-infinite-scroll-component";

export const InfiniteList = ({ data, getMore }) => {
    const [items, setPosts] = useState(data);
    
    const getMorePost = async () => {
        const newPosts = await getMore();
        setPosts((post) => [...post, ...newPosts]);
    };

    return (
        <InfiniteScroll
            height={450}
            dataLength={items.length}
            next={getMorePost}
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
