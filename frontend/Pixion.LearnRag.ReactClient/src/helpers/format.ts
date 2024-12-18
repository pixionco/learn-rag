export function toPrettyJSON(data: object, indent = 2) {
  return JSON.stringify(data, null, indent);
}
