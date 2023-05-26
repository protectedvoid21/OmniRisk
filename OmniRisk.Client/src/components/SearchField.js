import { SearchControl, OpenStreetMapProvider } from "leaflet-geosearch";
import { useMap } from "react-leaflet";
import { useEffect } from "react";

const SearchField = ({ apiKey }) => {
  // @ts-ignore

  const map = useMap();
  useEffect(() => {
    const provider = new OpenStreetMapProvider({
      params: {
        email: "pietrusjakub@gmail.com",
      },
    });

    const searchControl = new SearchControl({
      provider: provider,
      style: "bar",
      searchLabel: "Find your new match",
      autoClose: true,
    });

    map.addControl(searchControl);
    return () => map.removeControl(searchControl);
  }, [map]);

  return null;
};

export default SearchField;
