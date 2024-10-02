import { ConfigService } from "../services/applicationServices/config.service";

export const configFactory = (configService: ConfigService) => {
  return () => configService.loadConfig();
};