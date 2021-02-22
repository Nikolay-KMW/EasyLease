export interface RegisterRequestInterface {
  user: {
    userName: string;
    email: string;
    password: string;
    confirmPassword: string;
  };
}
