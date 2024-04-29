export interface Event {
  id: number;
  type: string;
  description: string;
  location: string;
  date: string;
}

export interface State {
  events: Event[];
}
