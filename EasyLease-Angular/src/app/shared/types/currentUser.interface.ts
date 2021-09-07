export interface CurrentUserInterface {
  id: string;
  userName: string;
  firstName: string;
  secondName: string;
  thirdName: string;
  email: string;
  bio: string | null;
  createdUser: string;
  updatedUser: string | null;
  visitedUser: string;
  token: string | null;
  image: string | null;
}
