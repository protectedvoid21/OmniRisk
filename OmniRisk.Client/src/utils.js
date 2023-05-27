import { getDistance } from "geolib";

export const getDistanceBetweenTwoPoints = (point1, point2) => {
  return (getDistance(point1, point2) / 1000).toFixed(1);
};
