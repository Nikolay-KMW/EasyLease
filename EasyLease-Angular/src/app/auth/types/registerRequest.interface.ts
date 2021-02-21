export interface RegisterRequestInterface {
  user: {
    username: string;
    email: string;
    password: string;
    confirmPassword: string;
  };
}
